using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
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
            ViewBag.RoleList = new BaseBLL<SYS_Role>().GetAll();
            ViewBag.AppList = new BaseBLL<WX_App>().GetAll();
            return PartialView("AddSYS_User");
        }

        [HttpPost]
        public ActionResult AddSysUser(SYS_User user)
        {
            user.UserState = "正常";
            if (new BaseBLL<SYS_User>().Add(user))
            {
                return Redirect("/Base/Home/SysUser");
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


    }
}