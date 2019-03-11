using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXService.Utility;

namespace WXService.Service
{
    public class DataService : BaseService
    {
        private const string GETUSERSUMMARY = "https://api.weixin.qq.com/datacube/getusersummary?access_token={0}";
        private const string GETUSERCUMULATE = "https://api.weixin.qq.com/datacube/getusercumulate?access_token={0}";
        public DataService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }

        public string GetUserSummary(int day)
        {
            //var begin_date = DateTime.Now.AddDays(-1);
            //var end_date = DateTime.Now.AddDays(-1).AddDays(-1 - day);
            var json = new {
                begin_date = DateTime.Now.AddDays(-day).ToShortDateString(),
                end_date = DateTime.Now.AddDays(-1).ToShortDateString(),
            };
            string url = string.Format(GETUSERSUMMARY, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;


        }

        public string GetUserCumulate(int day)
        {

            //var begin_date = DateTime.Now.AddDays(-1);
            //var end_date = DateTime.Now.AddDays(-1).AddDays(-1 - day);
            var json = new
            {
                begin_date = DateTime.Now.AddDays(-day).ToShortDateString(),
                end_date = DateTime.Now.AddDays(-1).ToShortDateString(),
            };
            string url = string.Format(GETUSERCUMULATE, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;


        }

    }
}
