using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WXDataUI.App_Start
{
    public class MobileHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public override Task OnConnected()
        {
            //Clients.All.hello("hello");
            return base.OnConnected();  
        }

    }
}