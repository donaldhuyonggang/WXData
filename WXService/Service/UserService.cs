using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WXService.Utility;

namespace WXService.Service
{
    /// <summary>
    /// 微信用户服务类
    /// </summary>
    public class UserService:BaseService
    {
        private const string USER_INFO_URL = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}";
        private const string USER_GET_URL = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";
        private const string USER_ADD_TAG_URL = "https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?access_token={0}";

        public UserService(string appId, string appSecert)
            :base(appId, appSecert)
        {

        }

        /// <summary>
        /// 根据openid获得用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string Info(string openId, string lang = "zh_CN ")
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(USER_INFO_URL, access_token,openId,lang);
            string respJson = MyHttpUtility.SendGet(url);
            return respJson;
        }

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="next_openid"></param>
        /// <returns></returns>
        public string Get(string next_openid="")
        {
            string access_token = this.Get_Access_Token();
            string url = string.Format(USER_GET_URL, access_token, next_openid);
            string respJson = MyHttpUtility.SendGet(url);
            return respJson;
        }

        public string AddTag(List<string> openIdList,int tagId)
        {
            if (openIdList.Count == 0) return "" ;
            var data = new
            {
                openid_list = openIdList,
                tagid=tagId
            };
            string url = string.Format(USER_ADD_TAG_URL, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(data));
            return respJson;
        }
    }
}
 