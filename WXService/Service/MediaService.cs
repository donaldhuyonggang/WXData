using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXService.Service
{
    public class MediaService : BaseService
    {
        private const string MEDIA_ADD_NEWS = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";
        private const string MEDIA_ADD_MATERIAL = "https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}";
        
        public MediaService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }


    }
}
