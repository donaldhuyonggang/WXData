using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataModel;

namespace WXDataUI.Areas.WXCustom.Controllers
{
    public class HomeController : Controller
    {
        // GET: WXCustom/Home
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 加载树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUser() {
            var list = new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.WX_User.SYS_User.UserId==1 || s.MsgType=="1").ToList();
            var Data = list.Select(s => new
            {   
                id=s.OpenID,
                text=s.WX_User.UserNick
            });
            return Json(Data,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo() {
            return PartialView();
        }

        /// <summary>
        /// 查看当前用户聊天
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TaleToUser(string id) {
           List<WX_Message> list=new WXDataBLL.WXCustom.WX_MessageManager().Where(s => ( s.ToUserName == id || s.FromUserName == id) &&( s.ToUserName.Equals("admin") || s.FromUserName.Equals("admin")));
           ViewBag.Talk = list;
           return PartialView();
        }

        /// <summary>
        /// 发送消息给用户
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TaleToUser(WX_Message msg) {
            bool IsTrue = new WXDataBLL.WXCustom.WX_MessageManager().Add(msg);
            return Json(IsTrue,JsonRequestBehavior.AllowGet);
        }



    }
}