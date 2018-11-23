using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.WXCustom;
using WXDataModel;
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

                WX_Queue info = new WX_Queue()
                {
                    MsgState = 0,
                    MsgId = XmlUtility.GetSingleNodeInnerText(xml, "/xml/MsgId"),
                    CreateTime = DateTimeUtility.Parse(createTime),
                    XmlContent = xml,
                    OpenID = XmlUtility.GetSingleNodeInnerText(xml, "/xml/FromUserName"),
                    MsgType = XmlUtility.GetSingleNodeInnerText(xml, "/xml/MsgType")
                };

                new WX_QueueManager().Add(info);

                return Content("success");
            }

        }
    }
}