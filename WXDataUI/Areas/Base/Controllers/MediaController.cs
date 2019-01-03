using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataModel;
using WXDataUI.Models;

namespace WXDataUI.Areas.Base.Controllers
{
    public class MediaController : Controller
    //public class MediaController : Controller_EX
    {
        // GET: Base/Media
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPublicMedia()
        {
            var list = new WX_MediaManager().Where(m => m.AppId == null);
            var json = list.Select(m => new
            {
                m.MyMediaId,
                m.MediaId,
                m.MediaName,
                m.MediaType,
                m.MediaContent,
                m.MediaState,
                m.UploadTime,
            });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult AddMedia()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddMedia(WX_Media media)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}