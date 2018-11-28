using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataModel;

namespace WXDataUI.Areas.Base.Controllers
{
    public class RoleController : Controller
    {
        // GET: Base/Role
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetRole()
        {
            SYS_User user = (Session["SYSUSER"] as SYS_User);
            var list = new BaseBLL<SYS_Role>().Where(s => (s.AppId == user.WX_App.AppId) || (string.IsNullOrEmpty(s.AppId)));
            var json = list.Select(s => new {
                s.RoleId,
                s.RoleSign,
                s.RoleName,
                Type = (s.AppId == null?"公共":s.WX_App.AppName)
            });
            return Json(json,JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult AddRole()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddRole(SYS_Role role)
        {
            if (role.AppId.Equals("-1"))
            {
                role.AppId = null;
            }
            if(new BaseBLL<SYS_Role>().Add(role))
            {
                return Redirect("/Base/Role/Index");
            }
            return Content("false");
        }

        [HttpGet]
        public ActionResult UpdateRole(int id)
        {
            ViewBag.role = new BaseBLL<SYS_Role>().GetByPK(id);
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateRole(SYS_Role role)
        {
            if (role.AppId.Equals("-1"))
            {
                role.AppId = null;
            }
            if (new BaseBLL<SYS_Role>().Update(role))
            {
                return Redirect("/Base/Role/Index");
            }
            return Content("false");
        }


        [HttpGet]
        public ActionResult EditRight()
        {
            ViewBag.FuncList = new BaseBLL<SYS_Function>().GetAll();

            return PartialView();
        }
    }
}