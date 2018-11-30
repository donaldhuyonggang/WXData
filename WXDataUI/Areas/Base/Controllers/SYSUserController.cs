using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.SYSRole;
using WXDataBLL.SYSUser;
using WXDataBLL.WXApp;
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
            ViewBag.AppList = new WX_AppManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSysUser(SYS_User user)
        {
            user.UserState = "正常";
            if (new SYS_UserManager().Add(user))
            {
                return Redirect("/Base/SYSUser/Index");
            }
            return Content("false");

        }

        [HttpGet]
        public ActionResult UpdateSysUser(int id)
        {
            ViewBag.SYSUser = new SYS_UserManager().GetByPK(id);
            ViewBag.RoleList = new SYS_RoleManager().GetAll();
            ViewBag.AppList = new WX_AppManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateSysUser(SYS_User user)
        {
            if (new SYS_UserManager().Update(user))
            {
                return Redirect("/Base/SYSUser/Index");
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