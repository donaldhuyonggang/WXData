using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataModel;
using WXService.Service;

namespace WXDataUI.Areas.WXMedia.Controllers
{
    public class TempController : Controller
    {
        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }
        // GET: WXMedia/Temp
        public ActionResult Index()
        {
            return View();
        }

        public void Upload()
        {
            //{{"type": "image",
            //"media_id": "_8K0nmhd75HbKmEfjVCi6NE1UxtlJ4oZPHiSEcKF56BpcOkxMt79hVL1msWuB9Dp",
            //"created_at": 1552306835
            //} }
            var str = new MediaService(WXAPP.AppId, WXAPP.AppSecret).UploadTemp("111.jpg", "image");
        }

        public void Get()
        {
            var str = new MediaService(WXAPP.AppId, WXAPP.AppSecret).GetTempUrl("_8K0nmhd75HbKmEfjVCi6NE1UxtlJ4oZPHiSEcKF56BpcOkxMt79hVL1msWuB9Dp");
        }
    }
}