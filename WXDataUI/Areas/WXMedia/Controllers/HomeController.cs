using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.WXCustom;
using WXDataModel;
using WXDataModel.Extend;
using WXService.Service;

namespace WXDataUI.Areas.WXMedia.Controllers
{
    public class HomeController : Controller
    {
        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }
        // GET: WXMedia/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddMedia()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddMedia(WX_Media media)
        {

            return PartialView();
        }
        
        public void Send(string text)
        {
            List<string> openIdList = WXAPP.WX_User.ToList().GetOpenIdList();
            string result = new MessageService(WXAPP.AppId,WXAPP.AppSecret).Send(openIdList, text);
            WX_CustomMsgManager manager = new WX_CustomMsgManager();
            JObject json = JObject.Parse(result);
            if (json["errcode"].ToString().Equals("0"))
            {
                foreach (var i in openIdList)
                {
                    WX_CustomMsg msg = new WX_CustomMsg()
                    {
                        MsgId = json["msg_id"].ToString(),
                        OpenID = i,
                        AppId = WXAPP.AppId,
                        CreateTime = DateTime.Now,
                        Content = text,
                        MsgSource = "客服",
                        MsgType = "text",

                    };
                    manager.Add(msg);
                }
            }
            
        }

    }
}