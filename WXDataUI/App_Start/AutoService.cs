using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WXDataBLL.WXApp;
using WXDataBLL.WXCustom;
using WXDataBLL.WXEvent;
using WXDataModel;
using WXService.Service;

namespace WXDataUI.App_Start
{
    /// <summary>
    /// 启动自动服务
    /// </summary>
    public class AutoService
    {
        WX_EventQueueManager eventQueueBLL = new WX_EventQueueManager();
        WX_AppManager appBLL = new WX_AppManager();
        WX_UserManager userBLL = new WX_UserManager();

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
                            Subscribe(item);
                            break;
                        case "unsubscribe":
                           // UnSubscribe(item);
                            break;
                    }
                    
                }
            }
        }


        public void UnSubscribe(WX_EventQueue info)
        {
            WX_User modal = new WX_User()
            {
                AppId = info.AppId,
                UnSubscribeTime = DateTime.Now,
                UserState= "退订",
            };

            userBLL.Update(modal);
        }

        public void Subscribe(WX_EventQueue info)
        {
            var app = appBLL.GetByPK(info.AppId);
            if (app != null) { 
                CustomService customSvr = new CustomService(app.AppId,app.AppSecret);
                customSvr.SendText(info.OpenID, "欢迎您关注公众号");
                info.MsgState = 2;
                eventQueueBLL.Update(info);//把事件改为已处理


                UserService userSvr = new UserService(app.AppId, app.AppSecret);

                string json = userSvr.Info(info.OpenID);
                JObject jo = JObject.Parse(json);

                WX_User modal = new WX_User()
                {
                    AppId = info.AppId,
                    OpenID = jo["openid"].ToString(),
                    UserNick = jo["nickname"].ToString(),
                    UserSex = jo["sex"].ToString() == "1" ? "男" : "女",
                    City = jo["city"].ToString(),
                    Country = jo["country"].ToString(),
                    Province = jo["province"].ToString(),
                    Language = jo["language"].ToString(),
                    HeadImageUrl = jo["headimgurl"].ToString(),
                    SubscribeTime = DateTime.Now,
                    UserState = "正常",
                    Remark = jo["remark"].ToString(),
                };
                if(userBLL.GetByPK(modal.OpenID) ==null)
                {
                    userBLL.Add(modal);
                }
                else
                {
                    userBLL.Update(modal);
                }
               

            }
        }

    }
}