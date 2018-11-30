using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.SYSRole;
using WXDataBLL.SYSUser;
using WXDataModel;

namespace WXDataUI.Areas.Base.Controllers
{
    public class SYSUserController : Controller
    {
        // GET: Base/SYSUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddSysUser()
        {
            ViewBag.RoleList = new SYS_RoleManager().GetAll();
            ViewBag.AppList = new SYS_RoleManager().GetAll();
            return PartialView("AddSYS_User");
        }

        [HttpPost]
        public ActionResult AddSysUser(SYS_User user)
        {
            user.UserState = "正常";
            if (new SYS_UserManager().Add(user))
            {
                return Redirect("/Base/Home/SysUser");
            }
            return Content("false");

        }

        public ActionResult GetUsers()
        {
            var list = new SYS_UserManager().GetAll();
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


    }
}