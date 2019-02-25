using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXService.Utility;

namespace WXService.Service
{
    public class MenuService : BaseService
    {
        //
        private const string MENU_CREATE_URL = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
        private const string MENU_GET_URL = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";
        private const string MENU_DELETE_URL = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";

        public MenuService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }

        public string Get()
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(MENU_GET_URL, access_token);
            string respJson = MyHttpUtility.SendGet(url);
            return respJson;
        }

        public string Delete()
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(MENU_DELETE_URL, access_token);
            string respJson = MyHttpUtility.SendGet(url);
            return respJson;
        }
        public string Create(JObject json)
        {
            string s = Delete();
            string access_token = this.Get_Access_Token();
            string url = string.Format(MENU_CREATE_URL, access_token);
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;
        }


    }
}
