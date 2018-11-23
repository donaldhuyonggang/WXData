using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WXDataUI.Areas.Base.Controllers
{
    public class HomeController : Controller
    {
        // GET: Base/Home
        public ActionResult Index()
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
            return PartialView("Modal/Sys_UserUpdate");
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