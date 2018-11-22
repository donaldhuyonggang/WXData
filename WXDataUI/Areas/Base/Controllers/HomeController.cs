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
    }
}