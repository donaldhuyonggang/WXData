using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WXDataUI.Areas.WXUser.Controllers
{
    public class TagController : Controller
    {
        // GET: WXUser/Tag
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddTag()
        {
            return PartialView();
        }
    }
}