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
    /// <summary>
    /// 微信二维码服务类。
    /// </summary>
    public class QRService:BaseService
    {
        private const string QRCODE_CREATE_URL = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
        private const string SHOWQRCODE_URL = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";


        public QRService(string appId, string appSecert)
            :base(appId, appSecert)
        {

        }

        
        public string Create(int expire_seconds,string action_name,int scene_id,string scene_str)
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(QRCODE_CREATE_URL, access_token);
            var json = new
            {
                expire_seconds = expire_seconds,
                action_name = action_name,
                action_info = new
                {
                    scene = new { scene_id = scene_id }
                }
            };
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;

        }


        public string ShowQR(string ticket)
        {
            string url = string.Format(SHOWQRCODE_URL, ticket);
            //string respJson = MyHttpUtility.SendGet(url);
            return url;
        }
    }
}
