using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataModel;
using WXService.Service;

namespace WXDataUI.Areas.WXCustom.Controllers
{
    public class QRController : Controller
    {
        // GET: WXCustom/QR
        public ActionResult Index()
        {
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            ViewBag.date = new WXDataBLL.WXQR.WX_QRManager().Where(s=>s.AppId== SYSUSER.AppId&&s.UserId==SYSUSER.UserId);
            return View();
        }

        /// <summary>
        /// 用户添加二维码
        /// </summary>
        /// <param name="WQR"></param>
        /// <returns></returns>
        public ActionResult CreateQR(WX_QR WQR) {
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            WX_QR q = new WX_QR();
            q.AppId = SYSUSER.WX_App.AppId;
            q.UserId = SYSUSER.UserId;
            q.QRName = WQR.QRName;
            q.Expire_Seconds = 2592000;

            QRService qrSvr = new QRService(SYSUSER.WX_App.AppId, SYSUSER.WX_App.AppSecret);
            string json = qrSvr.Create(2592000, "QR_SCENE", SYSUSER.UserId, "");

            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            string ticket = jo["ticket"].ToString();
            q.Ticket = ticket;
            string url = qrSvr.ShowQR(ticket);
            q.QR_URL = url;
            q.CreateTime = DateTime.Now;

            var date = new WXDataBLL.WXQR.WX_QRManager().Add(q);

            return Redirect(url);

            
        }
    }
}