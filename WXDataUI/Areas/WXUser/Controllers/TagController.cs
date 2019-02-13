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
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            ViewBag.TagList = app.WX_UserTag.ToList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult DeleteTag(int tagid)
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            TagService ser = new TagService(app.AppId, app.AppSecret);
            JObject jo = JObject.Parse(ser.Delete(tagid));
            var result = new
            {
                errcode = jo["errcode"],
                errmsg = jo["errmsg"]
            };
            if (result.errcode.Equals(0))
            {
                new WX_UserTagManager().Delete(tagid);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EditTag()
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            ViewBag.TagList = app.WX_UserTag.ToList();
            return PartialView();
        }
        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTag(WX_UserTag tag)
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            TagService ser = new TagService(app.AppId, app.AppSecret);
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
            var list = GetTagList().Select(t => new
            {
                t.TagId,
                t.TagName,
                count = t.WX_User.Count()
            });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 从服务器更新标签集合
        /// </summary>
        /// <returns></returns>
        public List<WX_UserTag> GetTagList()
        {
            WX_UserTagManager manager = new WX_UserTagManager();
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            TagService ser = new TagService(app.AppId, app.AppSecret);
            List<WX_UserTag> list = new List<WX_UserTag>();
            JToken jo = JObject.Parse(ser.GetList())["tags"];
            foreach (var i in jo.Children())
            {
                var tag = new WX_UserTag()
                {
                    TagId = (int)i["id"],
                    TagName = i["name"].ToString(),
                    AppId = app.AppId
                };
                var info = manager.GetAll().Where(t => t.TagId == Convert.ToInt32(i["id"]) && t.AppId.Equals(app.AppId));
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
                manager.Delete(id);
            }

            Controller_EX.BindSession(Session);
            return list;
        }
    }
}