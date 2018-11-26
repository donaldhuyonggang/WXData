using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return PartialView("Sys_UserUpdate");
        }


        [HttpPost]
        public ActionResult Login(SYS_User info)
        {
            SYS_User user = new SYS_UserManager().Login(info);
            if (user!=null)
            {
                Session.Add("User",user);
                Session.Add("UserName", user.UserName);
                return Redirect("/Base/Home/Index");
            }
            return Content("false");
        }
        //public ActionResult GetUsers()
        //{
        //    var list = SYS_UserManager.QueryAll();
        //    var json = list.Select(u => new
        //    {
        //        u.UserId,
        //        u.LoginId,
        //        u.UserName,
        //        u.UserState,
        //        u.Telphone,
        //        u.Email,
        //        RoleName = u.SYS_Role.RoleName
        //    });
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}
    }
}