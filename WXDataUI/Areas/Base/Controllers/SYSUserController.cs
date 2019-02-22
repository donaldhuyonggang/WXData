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

namespace WXDataUI.Areas.Base.Controllers
{
    /// <summary>
    /// 系统用户管理控制器
    /// </summary>
    [RightFilter(Message = "系统用户管理")]
    public class SYSUserController : Controller
    {
        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }
        // GET: Base/SYSUser
        public ActionResult Index()
        {
            ViewBag.RoleList = new SYS_RoleManager().Where(r => (r.AppId == WXAPP.AppId) || (r.AppId == null));
            return View();
        }

        [HttpGet]
        public ActionResult AddSysUser()
        {
            ViewBag.RoleList = new SYS_RoleManager().Where(r => (r.AppId == WXAPP.AppId) || (r.AppId == null));
            ViewBag.AppList = new WX_AppManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSysUser(SYS_User user)
        {
            user.UserState = "正常";
            var result = new ReturnResult() { ErrorMsg="添加失败!"};
            if (new SYS_UserManager().Where(u => u.LoginId.Equals(user.LoginId)).Count>0)
            {
                result.ErrorCode = "";
                result.ErrorMsg = "用户名已存在!";
                result.Result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result.Result = new SYS_UserManager().Add(user);
            
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        

        [HttpGet]
        public ActionResult UpdateSysUser(int id)
        {
            ViewBag.SYSUser = new SYS_UserManager().GetByPK(id);
            ViewBag.RoleList = new SYS_RoleManager().Where(r => (r.AppId == WXAPP.AppId) || (r.AppId == null));
            ViewBag.AppList = new WX_AppManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateSysUser(SYS_User user)
        {
            return Json( new SYS_UserManager().Update(user),JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetUsers(string sign = null, string key = null)
        {
            List<SYS_User> list = null;
            if (string.IsNullOrEmpty(sign))
            {
                list = new SYS_UserManager().GetAll();
            }
            else
            {
                list = new SYS_UserManager().Where(u => u.SYS_Role.RoleSign.Equals(sign));
            }
            if(key != null)
            {
                JObject jo = JObject.Parse(key);
                if (!string.IsNullOrEmpty(jo["Name"].ToString()))
                {
                    string name = jo["Name"].ToString();
                    list = list.Where(u => u.LoginId.Contains(name) || u.UserName.Contains(name)).ToList();
                }
                if (!string.IsNullOrEmpty(jo["Email"].ToString()))
                {
                    string email = jo["Email"].ToString();
                    list = list.Where(u => u.Email != null && u.Email.Contains(email)).ToList();
                }
            }

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
        public ActionResult LookSysUser(int id) {
            ViewBag.LookSysUser = new SYS_UserManager().GetByPK(id);
            ViewBag.RoleList = new SYS_RoleManager().GetAll();
            ViewBag.AppList = new WX_AppManager().GetAll();
            return PartialView();
        }

        public ActionResult GetRoleByAppId(string appId)
        {
            var list = new SYS_RoleManager().Where(s => !s.RoleSign.Equals("SYS_ADMIN") && (s.AppId == null || s.AppId.Equals(appId))).Select(s => new
            {
                s.RoleId,
                s.RoleName
            });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}