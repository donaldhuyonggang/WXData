using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXDataModel.Extend
{
    public static class WX_MenuEvent_EX
    {
        public static string GetXML(this WX_MenuEvent e, string toUserName,string fromUserName)
        {
            string resultXml = "";
            switch (e.ResponType)
            {
                case "text":
                    resultXml = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>", fromUserName, toUserName, DateTime_EX.GetTimeStamp(), e.ResponContent);
                    break;
                case "news":
                    WX_Media_News news = new WXDataEntities().WX_Media.Where(i => i.AppId.Equals(e.AppId) && i.MediaId.Equals(e.ResponContent)).FirstOrDefault().TransitionToNews();
                    resultXml = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[news]]></MsgType><ArticleCount>1</ArticleCount><Articles><item><Title><![CDATA[{3}]]></Title><Description><![CDATA[{4}]]></Description><PicUrl><![CDATA[{5}]]></PicUrl><Url><![CDATA[{6}]]></Url></item></Articles></xml>",fromUserName,toUserName, DateTime_EX.GetTimeStamp(),
                        news.title,
                        news.digest,
                        news.GetPicUrl(),
                        news.url);
                    break;
                //case "voice":
                //    string format = XmlTools.GetNodeInnerText(xml, "/xml/MediaId");
                //    resultXml = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[voice]]></MsgType><Voice><MediaId><![CDATA[{3}]]></MediaId></Voice></xml>", fromUserName, toUserName, createTime, format);
                //    break;
                //case "image":
                //    string mediaId = XmlTools.GetNodeInnerText(xml,  "/xml/MediaId");
                //    resultXml = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[image]]></MsgType><Image><MediaId><![CDATA[{3}]]></MediaId></Image></xml>", fromUserName, toUserName, createTime, mediaId);
                //    break;
                //case "video":
                //    string video = XmlTools.GetNodeInnerText(xml, "/xml/MediaId");
                //    string thumbMediaId = XmlTools.GetNodeInnerText(xml, "/xml/ThumbMediaId");
                //    resultXml = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[video]]></MsgType><Video><MediaId><![CDATA[{3}]]></MediaId><Title><![CDATA['Test']]></Title><Description><![CDATA['描述']]></Description></Video></xml>", fromUserName, toUserName, createTime, video);
                //    break;
            }
            return resultXml;
        }
    }
}
