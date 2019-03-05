using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(WXDataUI.App_Start.MobileStartup))]

namespace WXDataUI.App_Start
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            var customId= request.QueryString["customId"];
            return customId; 
        }
    }

    public class MobileStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new UserIdProvider());
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
