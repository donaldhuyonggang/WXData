using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataBLL.WXUser;
using WXDataModel;
using WXDataUI.Models;
using WXService.Service;

namespace WXDataUI.Areas.WXUser.Controllers
{
    public class TagController : Controller
    {

        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }





        //public object TagServices { get; private set; }

        // GET: WXUser/Tag
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddTag()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddTag(string appId, string tagName)
        {
            WX_App app = new WX_AppManager().GetByPK(appId);
            TagService ser = new TagService(app.AppId, app.AppSecret);
            string json = ser.Create(tagName);
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            WX_UserTag tag = new WX_UserTag()
            {
                AppId = appId,
                TagId = (int)jo["tag"]["id"],
                TagName = jo["tag"]["name"].ToString(),
            };
            ReturnResult result = new ReturnResult() { Result = true };
            if(!new WX_UserTagManager().Add(tag))
            {
                result.Result = false;
                result.ErrorMsg = "新增失败!";
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteTag()
        {
            ViewBag.TagList = WXAPP.WX_UserTag.ToList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult DeleteTag(int tagid)
        {
            TagService ser = new TagService(WXAPP.AppId, WXAPP.AppSecret);
            JObject jo = JObject.Parse(ser.Delete(tagid));
            var result = new
            {
                errcode = jo["errcode"].ToString(),
                errmsg = jo["errmsg"].ToString()
            };
            if (result.errcode.Equals("0"))
            {
                new WX_UserTagManager().Delete(tagid,WXAPP.AppId);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EditTag()
        {
            ViewBag.TagList = WXAPP.WX_UserTag.ToList();
            return PartialView();
        }
        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTag(WX_UserTag tag)
        {
            TagService ser = new TagService(WXAPP.AppId, WXAPP.AppSecret);
            GetTagList();
            JObject jo = JObject.Parse(ser.Update(tag.TagId, tag.TagName));
            var result = new
            {
                errcode = jo["errcode"],
                errmsg = jo["errmsg"]
            };
            return Json(result,JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 更新标签
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SyncTag()
        {
            GetTagList();
            var TagList = new
            {
                data = new WX_UserTagManager().GetTagList(WXAPP.AppId).Select(s => new
                {
                    s.TagId,
                    s.TagName,
                    UserCount = s.WX_User.Count,
                }),
                UnTagCount = WXAPP.WX_User.Where(u => u.WX_UserTag.Count == 0).Count(),
                AllCount = WXAPP.WX_User.Count()
            };
            return Json(TagList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 从服务器更新标签集合
        /// </summary>
        /// <returns></returns>
        public List<WX_UserTag> GetTagList()
        {
            WX_UserTagManager manager = new WX_UserTagManager();
            TagService ser = new TagService(WXAPP.AppId, WXAPP.AppSecret);
            List<WX_UserTag> list = new List<WX_UserTag>();
            JToken jo = JObject.Parse(ser.GetList())["tags"];
            foreach (var i in jo.Children())
            {
                var tag = new WX_UserTag()
                {
                    TagId = (int)i["id"],
                    TagName = i["name"].ToString(),
                    AppId = WXAPP.AppId
                };
                var info = manager.GetAll().Where(t => t.TagId == Convert.ToInt32(i["id"]) && t.AppId.Equals(WXAPP.AppId));
                if (info.Count() > 0)
                {
                    //info.TagName = tag.TagName;
                    manager.Update(tag);
                }else
                {
                    manager.Add(tag);
                }
                list.Add(tag);
            }

            var idList = new List<int>();
            foreach (var i in manager.GetAll())
            {
                if(list.Where(t => t.TagId.Equals(i.TagId)).Count() == 0)
                {
                    idList.Add(i.TagId);
                }
            }
            foreach (var id in idList)
            {
                manager.Delete(id,WXAPP.AppId);
            }

            Controller_EX.BindSession(Session);
            return list;
        }
    }
}