using Newtonsoft.Json;
using WXService.Utility;

namespace WXService.Service
{
    public class TagService : BaseService
    {
        private const string TAG_CREATE_URL = "https://api.weixin.qq.com/cgi-bin/tags/create?access_token={0}";
        private const string TAG_GET_URL = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token={0}";
        public TagService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }
        public string Create(string TagName)
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(TAG_CREATE_URL, access_token);
            var json = new
            {
                tag = new
                {
                    name = TagName
                }
            };
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            
            return respJson;
        }
        public string GetList()
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(TAG_GET_URL, access_token);
            string respJson = MyHttpUtility.SendGet(url);
            return respJson;
        }

    }
}
