using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WXDataBLL.WXApp;
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

        public void Start()
        {
            System.Threading.Thread th = new System.Threading.Thread(this.Service);
            th.Start();//启动一个自动服务。
        }

        public void Service()
        {
         
            while (true)
            {
                var list = eventQueueBLL.Where(x => x.MsgState ==1);

                foreach (var item in list)
                {
                    switch (item.Event)
                    { 
                        case "subscribe":
                            Subscribe(item);
                            break;
                    }
                    
                }
            }
        }


        public void Subscribe(WX_EventQueue info)
        {
            var app = appBLL.GetByPK(info.AppId);
            if (app != null) { 
                CustomService customSvr = new CustomService(app.AppId,app.AppSecret);
                customSvr.SendText(info.OpenID, "欢迎您关注公众号");
                info.MsgState = 2;
                eventQueueBLL.Update(info);//把事件改为已处理
            }
        }

    }
}