using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.Base;
using WXDataModel;
using WXDataModel.Extend;
using WXDataUI.Models;
using WXService.Service;

namespace WXDataUI.Areas.Base.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
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

        [HttpPost]
        public ActionResult Login(SYS_User info)
        {
            
            SYS_User user = new SYS_UserManager().Login(info);
            if (user!=null)
            {
                Session["SYSUSER"] = user;
                Controller_EX.BindSession(Session);
                return Json(true, JsonRequestBehavior.AllowGet);
                //return Redirect("/Base/Home/Index");
            }
            //return JavaScript("<script>alert('登录失败!');location.href='/Base/Home/Login'</script>");
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            
            Session.Clear();
            
            return Redirect("/Base/Home/Login");
        }

        public ActionResult GetFunction()
        {
            var json = new SYS_FunctionManager().GetFunction((Session["SYSUSER"] as SYS_User).UserId);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        
    }
}