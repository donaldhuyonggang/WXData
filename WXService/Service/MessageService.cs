using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;
using WXService.Utility;

namespace WXService.Service
{
    public class MessageService : BaseService
    {
        private const string MESSAGE_SEND_URL = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
        private const string MESSAGE_SENDALL_URL = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
        public MessageService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }

        /// <summary>
        /// 群发文本
        /// </summary>
        /// <param name="openIdList"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string SendText(List<string> openIdList,string text)
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

        /// <summary>
        /// 群发语音
        /// </summary>
        /// <param name="openIdList"></param>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public string Voice(int tagId,string media_id) {
            var data = new
            {
                filter = new
                {
                    is_to_all = false,
                    tag_id = tagId
                },
                voice = new
                {
                    media_id = media_id,
                },
                msgtype = "voice"
            };
            string url = string.Format(MESSAGE_SENDALL_URL, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(data));
            return respJson;
        }

        /// <summary>
        /// 群发图片
        /// </summary>
        /// <param name="openIdList"></param>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public string Image(int tagId, string media_id) {
            var data = new
            {
                filter = new
                {
                    is_to_all = false,
                    tag_id = tagId
                },
                image = new
                {
                    media_id = media_id,
                },
                msgtype = "image"
            };
            string url = string.Format(MESSAGE_SENDALL_URL, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(data));
            return respJson;
        }





        public string Send(string MediaId,int tagId)
        {
            var data = new
            {
                filter = new
                {
                    is_to_all = false,
                    tag_id = tagId
                },
                mpnews = new
                {
                    media_id = MediaId,
                },
                msgtype = "mpnews",
                send_ignore_reprint = 0,
            };
            string url = string.Format(MESSAGE_SENDALL_URL, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(data));
            return respJson;
        }

        public string Send(WX_Media news)
        {
            var data = new
            {
                filter = new
                {
                    is_to_all = true,
                    //tag_id = 2
                },
                mpnews = new
                {
                    media_id = news.MediaId,
                },
                msgtype = "mpnews",
                send_ignore_reprint = 0,
            };
            string url = string.Format(MESSAGE_SENDALL_URL, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(data));
            return respJson;
        }

        public string SendImage(WX_Media media,int tag)
        {
            var data = new
            {
                filter = new
                {
                    is_to_all = false,
                    tag_id = tag
                },
                image = new 
                {
                    media_id = media.MediaId,
                },
                msgtype = "image",
            };
            string url = string.Format(MESSAGE_SENDALL_URL, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(data));
            return respJson;
        }

      
    }
}
