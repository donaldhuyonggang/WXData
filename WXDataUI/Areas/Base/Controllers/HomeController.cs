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
        

        [HttpPost]
        public ActionResult Login(SYS_User info)
        {
            SYS_User user = new SYS_UserManager().Login(info);
            if (user!=null)
            {
                Session.Add("SYSUSER",user);
                return Redirect("/Base/Home/Index");
            }
            return Content("false");
        }
        
    }
}