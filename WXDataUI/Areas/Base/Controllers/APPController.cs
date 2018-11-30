using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.SYSRole;
using WXDataBLL.WXApp;
using WXDataModel;

namespace WXDataUI.Areas.Base.Controllers
{
    public class APPController : Controller
    {
        // GET: Base/APP
        public ActionResult Index()
        {
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
        public ActionResult GetAppList(string type = "1")
        {

            List<WX_App> list = null;
            if (type.Equals("1"))
            {
                list = new WX_AppManager().GetAll();
            }else
            {
                list = new WX_AppManager().Where(a => a.AppType.Equals(type));
            }
            var json = list.Select(s => new
            {
                s.AppType,
                s.AppName,
                s.AppId,
                s.WXId,
                s.CompanyName,
                s.AppState,
                s.Remark
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddApp()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddApp(WX_App app)
        {
            app.AppState = "正常";
            if (new WX_AppManager().Add(app))
            {
                return Redirect("/Base/Home/Index");
            }
            return Content("false");
        }

        [HttpGet]
        public ActionResult UpdateApp(string id)
        {
            ViewBag.App = new WX_AppManager().GetByPK(id);
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateApp(WX_App app)
        {
            if (new WX_AppManager().Update(app))
            {
                return Redirect("/Base/Home/Index");
            }
            return Content("false");
        }

        [HttpPost]
        public ActionResult DeleteApp(string id)
        {


            WX_App app = new WX_AppManager().GetByPK(id);
            app.AppState = "无效";
            return Json(new WX_AppManager().Update(app), JsonRequestBehavior.AllowGet);
        }

       
    }
}