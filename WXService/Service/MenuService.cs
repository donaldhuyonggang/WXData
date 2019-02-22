using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXService.Service
{
    public class MenuService : BaseService
    {
        //
        private const string MENU_CREATE_URL = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
        private const string MENU_GET_URL = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";
        private const string MENU_DELETE_URL = "https://api.weixin.qq.com/cgi-bin/delete/delete?access_token={0}";

        public MenuService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }



    }
}
