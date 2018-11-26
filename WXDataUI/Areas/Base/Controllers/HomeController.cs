using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.SYSUser;
using WXDataModel;

namespace WXDataUI.Areas.Base.Controllers
{
    public class HomeController : Controller
    {
        // GET: Base/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ToSysUser()
        {
            return View("SYS_User");
        }

        [HttpGet]
        public ActionResult InsertSysUser()
        {
            return PartialView("SYS_UserUpdate");
        }


        [HttpPost]
        public ActionResult Login(SYS_User info)
        {
            SYS_User user = new SYS_UserManager().Login(info);
            if (user!=null)
            {
                WX_App app = new BaseBLL<WX_App>().GetAll().First();
                Session.Add("User",user);
                Session.Add("UserName", user.UserName);
                Session.Add("App", app);
                Session.Add("AppName", app.AppName);
                return Redirect("/Base/Home/Index");
            }
            return Content("false");
        }
        public ActionResult GetUsers()
        {
            var list = new BaseBLL<SYS_User>().GetAll();
            var json = list.Select(u => new
            {
                u.UserId,
                u.LoginId,
                u.UserName,
                u.UserState,
                u.Telphone,
                u.Email,
                RoleName = u.SYS_Role.RoleName
            });
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ChangeApp()
        {
            ViewBag.AppList = new BaseBLL<WX_App>().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeApp(int appId)
        {
            WX_App app = new BaseBLL<WX_App>().GetByPK(appId);
            Session["App"] = app;
            Session["AppName"] = app.AppName;
            return Redirect("/Base/Home/Index");
        }

        [HttpPost]
        public ActionResult GetAppList()
        {
            List<WX_App> list = new BaseBLL<WX_App>().GetAll();
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

            return Json(json,JsonRequestBehavior.AllowGet);
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
            if (new BaseBLL<WX_App>().Add(app))
            {
                return Redirect("/Base/Home/Index");
            }
            return Content("false");
        }

        [HttpGet]
        public ActionResult UpdateApp(string id)
        {
            ViewBag.App = new BaseBLL<WX_App>().GetByPK(id);
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateApp(WX_App app)
        {
            if(new BaseBLL<WX_App>().Update(app))
            {
                return Redirect("/Base/Home/Index");
            }
            return Content("false");
        }

        [HttpPost]
        public ActionResult DeleteApp(string id)
        {
            WX_App app = new BaseBLL<WX_App>().GetByPK(id);
            app.AppState = "无效";
            return Json(new BaseBLL<WX_App>().Update(app), JsonRequestBehavior.AllowGet);
        }

    }
}