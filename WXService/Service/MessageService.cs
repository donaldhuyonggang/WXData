using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXService.Utility;

namespace WXService.Service
{
    public class MessageService : BaseService
    {
        private const string MESSAGE_SEND_URL = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
        public MessageService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }

        public string Send(List<string> openIdList,string text)
        {
            var data = new
            {
                touser = openIdList,
                msgtype = "text",
                text = new
                {
                    content = text
                }
            };
            string url = string.Format(MESSAGE_SEND_URL, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(data));
            return respJson;
        }
    }
}
