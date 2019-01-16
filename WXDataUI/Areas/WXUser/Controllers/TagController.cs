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
        public object TagServices { get; private set; }

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

        public List<WX_UserTag> GetTagList()
        {
            WX_UserTagManager manager = new WX_UserTagManager();
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            TagService ser = new TagService(app.AppId, app.AppSecret);
            List<WX_UserTag> list = new List<WX_UserTag>();
            JToken jo = JObject.Parse(ser.GetList())["tags"];
            foreach (var i in jo.Children())
            {
                WX_UserTag tag = new WX_UserTag()
                {
                    TagId = (int)i["id"],
                    TagName = i["name"].ToString(),
                };
                var info = manager.GetByPK(tag.TagId);
                if (info != null)
                {

                    info.TagName = tag.TagName;
                    //manager.Update(info);
                }else
                {
                    manager.Add(tag);
                }

                list.Add(tag);
            }
            return list;
        }
    }
}