using Newtonsoft.Json;
using WXService.Utility;

namespace WXService.Service
{
    /// <summary>
    /// 客服服务类。
    /// </summary>
    public class CustomService:BaseService
    {
        private const string CUSTOM_SEND_URL = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
        private const string CUSTOM_ADD_URL = "https://api.weixin.qq.com/customservice/kfaccount/add?access_token={0}";

        public CustomService(string appId, string appSecert)
            :base(appId, appSecert)
        {
            
        }
        /// <summary>
        /// 发送文本。
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string SendText(string openId, string text)
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(CUSTOM_SEND_URL, access_token);
            var json = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content=text
                }
            };
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;
        }
 
    }
}
