using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WXDataUI.Areas.WXCustom.Controllers
{
    public class HomeController : Controller
    {
        // GET: WXCustom/Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 用户个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo() {
            return PartialView();
        }



    }
}