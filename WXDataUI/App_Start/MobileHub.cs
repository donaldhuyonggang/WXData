using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WXDataModel;
using WXService.Utility;

namespace WXDataUI.App_Start
{

    public class MobileHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello("ni");
        }
        public void Send(string OpenId,int UserId,string AppId)
        {
            List<WX_Queue> list = new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.MsgState == 1 && s.OpenID.Equals(OpenId));
            List<WX_CustomMsg> msg = new List<WX_CustomMsg>();
            foreach (WX_Queue item in list)
            {
                WX_CustomMsg CM = new WX_CustomMsg();
                CM.MsgId = item.MsgId;
                CM.OpenID = item.OpenID;
                CM.UserId =UserId;
                CM.AppId = AppId;
                CM.CreateTime = item.CreateTime;
                CM.Content = XmlUtility.GetSingleNodeInnerText(item.XmlContent, "/xml/Content");
                CM.MsgSource = "粉丝";
                CM.MsgType = item.MsgType;
                CM.XmlContent = item.XmlContent;
                new WXDataBLL.WXCustom.WX_QueueManager().Delete(item.MsgId);//删除
                new WXDataBLL.WXCustom.WX_CustomMsgManger().Add(CM); //添加到数据库
                var info = new WXDataBLL.WXCustom.WX_CustomMsgManger().GetByPK(CM.MsgId);
                msg.Add(info);//添加到集合
            }
            Clients.User(UserId.ToString()).Send(msg);
        }

    }
}