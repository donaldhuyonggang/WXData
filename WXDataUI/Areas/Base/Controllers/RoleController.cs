using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.SYSRole;
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
            var list = new SYS_RoleManager().Where(s => (s.AppId == user.WX_App.AppId) || (string.IsNullOrEmpty(s.AppId)));
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
            if(new SYS_RoleManager().Add(role))
            {
                return Redirect("/Base/Role/Index");
            }
            return Content("false");
        }

        [HttpGet]
        public ActionResult UpdateRole(int id)
        {
            ViewBag.role = new SYS_RoleManager().GetByPK(id);
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateRole(SYS_Role role)
        {
            if (role.AppId.Equals("-1"))
            {
                role.AppId = null;
            }
            if (new SYS_RoleManager().Update(role))
            {
                return Redirect("/Base/Role/Index");
            }
            return Content("false");
        }


        [HttpGet]
        public ActionResult EditRight()
        {
            List<SYS_Function> FuncList =  new SYS_FunctionManager().Where(x=>x.ParentID==null);

            List<object> json = new List<object>();
            foreach (var item in FuncList)
            {
                var info = new
                {
                    id = item.FunctionID,
                    text = item.FunctionName,
                    children = new List<object>()
                };
                //var list = new SYS_FunctionManager().Where(x => x.ParentID == item.FunctionID);
                DG(item.FunctionID, info);

                json.Add(info);
            }
          

            return Json(json,JsonRequestBehavior.AllowGet);
        }



        private void DG(int functionId, dynamic info)
        {

            var list = new SYS_FunctionManager().Where(x => x.ParentID == functionId);

            foreach (var item in list)
            {
                var sub = new
                {
                    id = item.FunctionID,
                    text = item.FunctionName,
                    children = new List<object>()
                };
              
                DG(item.FunctionID, sub);

                info.children.Add(sub);

            }

        }


    }
}