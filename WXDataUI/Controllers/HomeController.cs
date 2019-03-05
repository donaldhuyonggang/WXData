using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.WXCustom;
using WXDataBLL.WXEvent;
using WXDataModel;
using WXService.Service;
using WXService.Utility;

namespace WXDataUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            if (this.Request.RequestType.ToLower() == "get")
            {
                string signature = Request.QueryString["signature"];
                string timestamp = Request.QueryString["timestamp"];
                string nonce = Request.QueryString["nonce"];
                string echostr = Request.QueryString["echostr"];
                return Content(echostr);
            }
            else
            {
                string xml = MyHttpUtility.ReadRequest(this.Request);
                int createTime = Convert.ToInt32(XmlUtility.GetSingleNodeInnerText(xml, "/xml/CreateTime"));
                string msgType = XmlUtility.GetSingleNodeInnerText(xml, "/xml/MsgType");
                if(msgType == "event")
                {//事件
                    WX_EventQueue info = new WX_EventQueue()
                    {
                        CreateTime= DateTimeUtility.Parse(createTime),
                        Event= XmlUtility.GetSingleNodeInnerText(xml, "/xml/Event"),
                        MsgState=1,
                        MsgType= msgType,
                        XmlContent=xml,
                        OpenID= XmlUtility.GetSingleNodeInnerText(xml, "/xml/FromUserName"),
                        AppId = id
                    };
                    new WX_EventQueueManager().Add(info);
                }
                else { //消息
                    WX_Queue info = new WX_Queue()
                    {
                        MsgState = 1,
                        MsgId = XmlUtility.GetSingleNodeInnerText(xml, "/xml/MsgId"),
                        CreateTime = DateTimeUtility.Parse(createTime),
                        XmlContent = xml,
                        OpenID = XmlUtility.GetSingleNodeInnerText(xml, "/xml/FromUserName"),
                        MsgType = msgType,
                        AppId= id
                    };
                    new WX_QueueManager().Add(info);
                }
                

                return Content("success");
            }

        }

        public ActionResult QR()
        {
            QRService qrSvr = new QRService("wxb51501fa9702675f", "a56e69ded9b5eab3579ce771f2f9a668");
            string json = qrSvr.Create(2592000, "QR_SCENE", 1, "");

            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            string ticket = jo["ticket"].ToString();
            string url = qrSvr.ShowQR(ticket);

            return Redirect(url);
        }

        public ActionResult Test() {
            return View();
        }
    }
}