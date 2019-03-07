using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataModel;
using WXDataModel.Extend;
using WXDataUI.Filter;
using WXDataUI.Helpers;
using WXService.Service;

namespace WXDataUI.Areas.WXMedia.Controllers
{

    [RightFilter(Message = "图文消息管理")]
    public class NewsController : Controller
    {
        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }
        // GET: WXMedia/News
        public ActionResult Index(int index = 1, int size = 3)
        {
            var list = new WX_MediaManager().Where(m => m.AppId.Equals(WXAPP.AppId) && (m.MediaType.Equals("news")));
            ViewBag.PageList = new PageList<WX_Media_News>(list.TransitionToNews(), index, size);
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ImgList = new WX_MediaManager().Where(m => m.AppId.Equals(WXAPP.AppId) && m.MediaType.Equals("image"));
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(WX_Media_News news) 
        {
            var json = new MediaService(WXAPP.AppId, WXAPP.AppSecret).AddNews(news);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sync()
        {
            var r = false;
            var json = JObject.Parse(new MediaService(WXAPP.AppId, WXAPP.AppSecret).Get("news"));
            int total_count = Convert.ToInt32(json["total_count"].ToString());
            int item_count = Convert.ToInt32(json["total_count"].ToString());
            if (total_count > item_count)    //如果总数大于单次获取的量,那么循环调用
            {

            }
            else
            {
                WX_MediaManager manager = new WX_MediaManager();
                foreach (var i in json["item"].Children())
                {
                    var jo = i["content"]["news_item"][0];
                    WX_Media_News news = new WX_Media_News()
                    {
                        title = jo["title"].ToString(),
                        thumb_media_id = jo["thumb_media_id"].ToString(),
                        show_cover_pic = Convert.ToInt32(jo["show_cover_pic"].ToString()),
                        author = jo["author"].ToString(), 
                        digest = jo["digest"].ToString(),
                        content = jo["content"].ToString(),
                        content_source_url = jo["content_source_url"].ToString(),
                        url = jo["url"].ToString(),
                    };

                    WX_Media media = new WX_Media() {
                        AppId = WXAPP.AppId,
                        MediaId = i["media_id"].ToString(),
                        MediaName = jo["title"].ToString(),
                        MediaType = "news",
                        MediaContent = JsonConvert.SerializeObject(news).Replace("\\", "")
                    };
                    var info = manager.Where(m => m.AppId.Equals(media.AppId) && m.MediaId.Equals(media.MediaId)).FirstOrDefault();
                    if (info != null)
                    {
                        media.MyMediaId = info.MyMediaId;
                        r = manager.Update(media);
                    }
                    else
                    {
                        r = manager.Add(media);
                    }
                }
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}