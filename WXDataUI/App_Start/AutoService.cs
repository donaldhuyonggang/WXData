using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WXDataBLL.Base;
using WXDataBLL.WXCustom;
using WXDataBLL.WXEvent;
using WXDataBLL.WXUser;
using WXDataModel;
using WXService.Service;
using WXService.Utility;

namespace WXDataUI.App_Start
{
    /// <summary>
    /// 启动自动服务
    /// </summary>
    public class AutoService
    {
        WX_EventQueueManager eventQueueBLL = new WX_EventQueueManager();
        WX_QueueManager QueueBLL = new WX_QueueManager();
        WX_AppManager appBLL = new WX_AppManager();
        WX_UserManager userBLL = new WX_UserManager();
        SYS_UserManager sys_UserBLL = new SYS_UserManager();

        public void Start()
        {
            System.Threading.Thread th = new System.Threading.Thread(this.Service);
            th.Start();//启动一个自动服务。
        }

        public void Service()
        {
            while (true)
            {
                var list = eventQueueBLL.Where(x => x.MsgState == 1);
               
                foreach (var item in list)
                {
                    switch (item.Event)
                    { 
                        case "subscribe":
                            var context = GlobalHost.ConnectionManager.GetHubContext<MobileHub>();
                            context.Clients.All.hello(1);
                            Subscribe(item);
                            break;
                        case "unsubscribe":
                           UnSubscribe(item);
                            break;
                        case "SCAN":
                            Scan(item);
                            break;
                    }
                    item.MsgState = 2;
                    eventQueueBLL.Update(item);//把事件改为已处理
                }
                List<WX_Queue> Queue = QueueBLL.GetAll().OrderByDescending(s=>s.CreateTime).ToList();
                if (Queue!=null)
                {
                    SelCustom(Queue);
                }
            }
        }
        public void SelCustom(List<WX_Queue> Queue)
        {
            var Result = Queue.GroupBy(s => s.OpenID).Select(s => new
            {
                Count = s.Count(),
                info = s.Select(x => new
                {
                    x.OpenID,
                    x.WX_User.HeadImageUrl,
                    x.WX_User.UserId,
                    x.WX_User.UserNick,
                    x.WX_User.UserName,
                    CreateTime = DateTimeUtility.DATE(Convert.ToDateTime(x.CreateTime)),
                    Content = XmlUtility.GetSingleNodeInnerText(x.XmlContent, "/xml/Content")
                }).FirstOrDefault()
            });
            foreach (var item in Result)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<MobileHub>();
                context.Clients.User(item.info.UserId.ToString()).PlusUse(Result);
            }
        }
        public void Scan(WX_EventQueue info)
        {
            string eventKey=XmlUtility.GetSingleNodeInnerText(info.XmlContent, "/xml/EventKey");
            WX_User modal = userBLL.GetByPK(info.OpenID);
            if (modal != null)
            {
                WX_User modal1 = new WX_User();
                EntityUntility.CopyProperty(modal, modal1);
                modal1.UserId = GetUserIdByScene(eventKey);
                userBLL.Update(modal1);
            }
            else
            {
                var app = appBLL.GetByPK(info.AppId);
                if(app != null) {
                    GetUserInfo(info.OpenID, app.AppId, app.AppSecret);
                }
                
            }
         }

        public void UnSubscribe(WX_EventQueue info)
        { 
            WX_User modal = userBLL.GetByPK(info.OpenID);
            if (modal != null)
            { 
                WX_User modal1 = new WX_User();
                EntityUntility.CopyProperty(modal, modal1); 
                modal1.UnSubscribeTime = DateTime.Now;
                modal1.UserState = "退订";
                userBLL.Update(modal1);
            }
               
        }

        public void Subscribe(WX_EventQueue info)
        {
            var app = appBLL.GetByPK(info.AppId);
            if (app != null) { 
                CustomService customSvr = new CustomService(app.AppId,app.AppSecret);
                customSvr.SendText(info.OpenID, "欢迎您关注公众号");
                GetUserInfo(info.OpenID, app.AppId, app.AppSecret);
            }
        }
        public void GetUserInfo(string openId,string appId,string appSecret)
        {
            UserService userSvr = new UserService(appId, appSecret);
            string json = userSvr.Info(openId);
            JObject jo = JObject.Parse(json); 
            WX_User modal = userBLL.GetByPK(openId);
            if (modal == null)
            {
                if (jo["subscribe"].ToString().Equals("0")) return;
                modal = new WX_User();
                modal.AppId = appId;
                modal.OpenID = openId;
                modal.UserNick = jo["nickname"].ToString();
                modal.UserSex = jo["sex"].ToString() == "1" ? "男" : "女";
                modal.City = jo["city"].ToString();
                modal.Country = jo["country"].ToString();
                modal.Province = jo["province"].ToString();
                modal.Language = jo["language"].ToString();
                modal.HeadImageUrl = jo["headimgurl"].ToString();
                modal.SubscribeTime = DateTime.Now;
                modal.UserState = "正常";
                modal.Remark = jo["remark"].ToString();
                //modal.GroupId = 1;//新用户
                modal.Subscribe_Scene = jo["subscribe_scene"].ToString();
                modal.QR_Scene = jo["qr_scene"].ToString();
                modal.QR_Scene_String = jo["qr_scene_str"].ToString();
                modal.UserId = GetUserIdByScene(jo["qr_scene"].ToString());
                userBLL.Add(modal);
            }
            else
            {
                WX_User modal1 = new WX_User();
                EntityUntility.CopyProperty(modal, modal1);
                modal1.AppId = appId;
                modal1.OpenID = openId;
                modal1.UserNick = jo["nickname"].ToString();
                modal1.UserSex = jo["sex"].ToString() == "1" ? "男" : "女";
                modal1.City = jo["city"].ToString();
                modal1.Country = jo["country"].ToString();
                modal1.Province = jo["province"].ToString();
                modal1.Language = jo["language"].ToString();
                modal1.HeadImageUrl = jo["headimgurl"].ToString();
                modal1.UserState = "正常";
                modal1.SubscribeTime = DateTime.Now;
                modal1.Remark = jo["remark"].ToString();
                modal1.Subscribe_Scene = jo["subscribe_scene"].ToString();
                modal1.QR_Scene = jo["qr_scene"].ToString();
                modal1.QR_Scene_String = jo["qr_scene_str"].ToString();
                modal1.UserId = GetUserIdByScene(jo["qr_scene"].ToString());
                userBLL.Update(modal1);
                
            }
        }
        public int? GetUserIdByScene(string scene)
        {
            int userId = 0;
            if(int.TryParse(scene,out userId))
            {
                if (sys_UserBLL.GetByPK(userId) != null)
                {
                    return userId;
                }
                else
                {
                    return null;
                }
            }
            else { return null; }

            
        }
    }
}