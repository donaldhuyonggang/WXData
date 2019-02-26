using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WXDataUI.Areas.WXMedia.Controllers
{
    public class NewsController : Controller
    {
        // GET: WXMedia/News
        public ActionResult Index()
        {
            return View();
        }
    }
}