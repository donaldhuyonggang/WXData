using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXDataModel.Extend
{
    public static class WX_Media_EX
    {
        public static WX_Media_News TransitionToNews(this WX_Media media)
        {
            if (media == null) return null;
            if (!media.MediaType.Equals("news")) return null;
            return Trans(media.MediaContent);
        }

        public static List<WX_Media_News> TransitionToNews(this List<WX_Media> list)
        {
            List<WX_Media_News> news = new List<WX_Media_News>();
            foreach (var i in list)
            {
                news.Add(Trans(i.MediaContent));
            }
            return news;
        }
        private static WX_Media_News Trans(string MediaContent)
        {
            var jo = JObject.Parse(MediaContent);
            WX_Media_News news = new WX_Media_News()
            {
                author = jo["author"].ToString(),
                content = jo["content"].ToString(),
                content_source_url = jo["content_source_url"].ToString(),
                digest = jo["digest"].ToString(),
                need_open_comment = Convert.ToInt32(jo["need_open_comment"]),
                only_fans_can_comment = Convert.ToInt32(jo["only_fans_can_comment"]),
                show_cover_pic = Convert.ToInt32(jo["show_cover_pic"]),
                thumb_media_id = jo["thumb_media_id"].ToString(),
                title = jo["title"].ToString(),
                url = jo["url"].ToString(),
            };
            return news;
        }

        public static string GetPicUrl(this WX_Media_News news)
        {
            return new WXDataEntities().WX_Media.Where(m => m.MediaId.Equals(news.thumb_media_id) && m.MediaType.Equals("image")).FirstOrDefault().MediaContent;
        }

        public static string GetMediaId(this WX_Media_News news)
        {
            var content = JsonConvert.SerializeObject(news).Replace("\\", "");
            return new WXDataEntities().WX_Media.Where(m => m.MediaContent.Equals(content)).FirstOrDefault().MediaId;
        }

        
    }
}
