using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.Base;
using WXDataModel;
using WXDataUI.Filter;
using WXDataUI.Models;
using WXService.Utility;

namespace WXDataUI.Areas.Base.Controllers
{
    /// <summary>
    /// 公众号控制器
    /// </summary>
    /// 
    [RightFilter(Message ="公众号管理")]
    public class APPController : Controller
    {
        public APPController()
        {
            
        }
        
        // GET: Base/APP
        public ActionResult Index()
        {
            ViewBag.TypeList = new WX_AppTypeManager().GetAll();
            return View();
        }


        [HttpGet]
        public ActionResult ChangeApp()
        {
            ViewBag.AppList = new WX_AppManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeApp(string appId)
        {
            WX_App app = new WX_AppManager().GetByPK(appId);
            Session["App"] = app;
            Session["AppName"] = app.AppName;
            return Redirect("/Base/Home/Index");
        }

        [HttpPost]
        public ActionResult GetAppList(int typeId = 0, string key = null)
        {

            List<WX_App> list = null;
            if (typeId == 0)
            {
                list = new WX_AppManager().GetAll();
            }else
            {
                list = new WX_AppManager().Where(a => a.TypeId == typeId);
            }
            if (!string.IsNullOrEmpty(key))
            {
                JObject jo = JObject.Parse(key);
                if (!string.IsNullOrEmpty(jo["AppName"].ToString()))
                {
                    string AppName = jo["AppName"].ToString();
                    list = list.Where(a => a.AppName.Contains(AppName)).ToList();
                }
                if (!string.IsNullOrEmpty(jo["WXId"].ToString()))
                {
                    string WXId = jo["WXId"].ToString();
                    list = list.Where(a => a.WXId.Contains(WXId)).ToList();
                }
                if (!string.IsNullOrEmpty(jo["CompanyName"].ToString()))
                {
                    string CompanyName = jo["CompanyName"].ToString();
                    list = list.Where(a => a.CompanyName != null && a.CompanyName.Contains(CompanyName)).ToList();
                }
            }
            var json = list.Select(s => new
            {
                AppType = s.WX_AppType.TypeName,
                s.AppName,
                s.AppId,
                s.WXId,
                s.CompanyName,
                s.AppState,
                s.Remark,
                s.TypeId
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddApp()
        {
            ViewBag.TypeList = new WX_AppTypeManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddApp(WX_App app)
        {
            app.AppState = "正常";
            ReturnResult result = new ReturnResult();
            result.ErrorMsg = "新增失败!";
            if (!CheckAppId(app.AppId))
            {
                result.ErrorMsg += "公众号Id重复!";
                result.Result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result.Result = new WX_AppManager().Add(app);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult UpdateApp(string id)
        {
            ViewBag.App = new WX_AppManager().GetByPK(id);
            ViewBag.TypeList = new WX_AppTypeManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateApp(WX_App app)
        {
            var result = new ReturnResult() { ErrorMsg="修改失败!",Result= new WX_AppManager().Update(app) };
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteApp(string id)
        {
            WX_AppManager bll = new WX_AppManager();
            WX_App app = bll.GetByPK(id);
            WX_App app1 = new WX_App();
            EntityUntility.CopyProperty(app, app1);
            app1.AppState = "无效";
            var result = new ReturnResult() { ErrorMsg = "修改状态失败!", Result = new WX_AppManager().Update(app1) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        private bool CheckAppId(string appId)
        {
            var list = new WX_AppManager().Where(a => a.AppId.Equals(appId));
            return list.Count() == 0;
        }
       
    }
}