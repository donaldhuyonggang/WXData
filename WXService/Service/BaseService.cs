/*
 *
 *
 *
 */


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WXService.Utility;

namespace WXService.Service
{
    /// <summary>
    /// 服务基类。
    /// </summary>
    public abstract class BaseService
    {
        private const string ACCESS_TOKEN_URL = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        //正式
        //private const string APPID = "wx19436f249abab2a0";
        //private const string APPSECRET = "e779734712028e172e1babb007ce275e";
        
        //测试
        private string APPID = "wxb51501fa9702675f";
        private string APPSECRET = "a56e69ded9b5eab3579ce771f2f9a668";
   
        private static string access_token = string.Empty;
        private static DateTime expires_Time = DateTime.Now.AddDays(-1);

        public BaseService(string appId,string appSecert)
        {
            APPID = appId;
            APPSECRET = appSecert;
        }

        internal string Get_Access_Token()
        {
            if (string.IsNullOrEmpty(access_token)
                && DateTime.Now > expires_Time)
            {
                string url = string.Format(ACCESS_TOKEN_URL, APPID, APPSECRET);
                string json = MyHttpUtility.SendGet(url);
                JObject jo = (JObject)JsonConvert.DeserializeObject(json);
                access_token = Convert.ToString(jo["access_token"].ToString());
                int expires_in = Convert.ToInt32(jo["expires_in"].ToString());
                expires_Time= DateTime.Now.AddSeconds(expires_in);
            }
            return access_token;
        }

    }
}
