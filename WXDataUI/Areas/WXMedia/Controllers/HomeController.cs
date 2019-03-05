using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataBLL.WXCustom;
using WXDataModel;
using WXDataModel.Extend;
using WXDataUI.Helpers;
using WXService.Service;

namespace WXDataUI.Areas.WXMedia.Controllers
{
    public class HomeController : Controller
    {
        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }
        // GET: WXMedia/Home
        public ActionResult Index(int index = 1,int size = 3)
        {
            //SyncImage();
            ViewBag.PageList = new PageList<WX_Media>(new WX_MediaManager().Where(m => m.AppId.Equals(WXAPP.AppId) && (!m.MediaType.Equals("news"))), index, size);
            return View();
        }

        public ActionResult GetPage(int index = 1, int size = 3)
        {
            var PageList = new PageList<WX_Media>(new WX_MediaManager().Where(m => m.AppId.Equals(WXAPP.AppId) && (!m.MediaType.Equals("news"))), index, size);
            return Json(PageList,JsonRequestBehavior.AllowGet);
        }

       [HttpGet]
        public ActionResult AddMedia()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddMedia(FormCollection form)
        {
            
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return Json("文件数为0上传不成功",JsonRequestBehavior.AllowGet);
            }

            var file = Request.Files["Media"];
            if (file.ContentLength == 0)
            {
                //文件大小（以字节为单位）为0时，做一些操作
                return Json("文件大小（以字节为单位）为0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                HttpPostedFileBase files = Request.Files[0];
                string fileName = (!string.IsNullOrEmpty(form["FileName"])) ? form["FileName"]: files.FileName;
                string format = files.FileName.Substring(files.FileName.LastIndexOf('.'));
                if(fileName.IndexOf(format) == -1)
                {
                    fileName += format;
                }
                //文件大小不为0
                string url = Server.MapPath("/Upload/") + fileName;
                files.SaveAs(url);
                MediaService ser = new MediaService(WXAPP.AppId,WXAPP.AppSecret);
                JObject jo = ser.PostImage(fileName); //返回一个mediaid和url
                if (jo != null)//新增成功
                {
                    var r = new WX_MediaManager().Add(new WX_Media()
                    {
                        AppId = WXAPP.AppId,
                        MediaId = jo["media_id"].ToString(),
                        MediaName = fileName,
                        MediaType = "image",
                        MediaContent = jo["url"].ToString(),
                        UploadTime = DateTime.Now,
                    });
                    
                }
            }
            return Redirect("Index");
        }

        public ActionResult SyncImage()
        {
            string json = new MediaService(WXAPP.AppId, WXAPP.AppSecret).Get("image");
            JObject jo = JObject.Parse(json);
            if (jo["errcode"] != null)
            {
                return Content("<script> alert('错误:"+jo["errmsg"].ToString() +"');</script>");
            }
            int total_count = Convert.ToInt32(jo["total_count"]); // 该类型素材总数
            int item_count = Convert.ToInt32(jo["item_count"]); // 本次获取的素材数量 
            if(total_count > item_count)    //如果总数大于单次获取的量,那么循环调用
            {

            }else
            {
                WX_MediaManager manager = new WX_MediaManager();
                foreach (var i in jo["item"].Children())
                {
                    var media = new WX_Media()
                    {
                        AppId = WXAPP.AppId,
                        MediaId = i["media_id"].ToString(),
                        MediaName = i["name"].ToString(),
                        MediaType = "image",
                        MediaContent = i["url"].ToString(),
                        //UploadTime = DateTime_EX.GetDateTime(Convert.ToInt32(i["update_Time"].ToString()))
                    };
                    var info = manager.GetByMediaId(media.MediaId, media.AppId);
                    if(info != null)
                    {
                        media.MyMediaId = info.MyMediaId;
                        var r = manager.Update(media);
                    }else
                    {
                        var r = manager.Add(media);
                    }
                }
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string mediaid)
        {
            WX_MediaManager manager = new WX_MediaManager();
            var json = JObject.Parse(new MediaService(WXAPP.AppId, WXAPP.AppSecret).Delete(mediaid));
            if (json["errcode"].ToString().Equals("0"))
            {
                var info = manager.GetByMediaId(mediaid, WXAPP.AppId);
                var r = manager.Delete(info.MyMediaId);
                return Json(r, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
            
        }



        /// <summary>
        /// 测试用群发消息
        /// </summary>
        /// <param name="text"></param>
        public void Send()
        {
            var news = new WX_MediaManager().Where(m => m.MediaType.Equals("news"))[0];
            List<string> openIdList = WXAPP.WX_User.ToList().GetOpenIdList();
            string result = new MessageService(WXAPP.AppId,WXAPP.AppSecret).Send(news);
            WX_CustomMsgManager manager = new WX_CustomMsgManager();
            JObject json = JObject.Parse(result);
            if (json["errcode"].ToString().Equals("0"))
            {
                foreach (var i in openIdList)
                {
                    WX_CustomMsg msg = new WX_CustomMsg()
                    {
                        MsgId = json["msg_id"].ToString(),
                        OpenID = i,
                        AppId = WXAPP.AppId,
                        CreateTime = DateTime.Now,
                        Content = news.MediaContent,
                        MsgSource = "客服",
                        MsgType = "text",

                    };
                    manager.Add(msg);
                }
            }
            
        }

    }
}