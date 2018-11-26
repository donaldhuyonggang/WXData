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
            if (Session["User"] == null)
            {
                return Redirect("/base/home/login");
            }
            else
            {
                return View();
            }
        }


        /// <summary>
        /// 加载树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUser()
        {
            SYS_User user = Session["User"] as SYS_User;
            var list = new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.WX_User.SYS_User.UserId == user.UserId || s.MsgType == "1").ToList();
            var Data = list.Select(s => new
            {
                id = s.OpenID,
                text = s.WX_User.UserNick
            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo(string id)
        {
            ViewBag.user = new WXDataBLL.WXCustom.WX_UserManager().GetByPK(id);
            return PartialView();
        }

        /// <summary>
        /// 查看当前用户聊天
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TaleToUser(string id)
        {
            ViewBag.OpenId = id;
            SYS_User user = Session["User"] as SYS_User;
            List<WX_Queue> list= new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.MsgState == 1&&s.OpenID.Equals(id));
            ViewBag.Talk = list;
            foreach (WX_Queue item in list)
            {
                item.MsgState = 2;
                new WXDataBLL.WXCustom.WX_QueueManager().Update(item);
            }
             //new WXDataBLL.WXCustom.WX_MessageManager().Where(s => s.ToUserName == id || s.FromUserName == id && (s.ToUserName.Equals(user.UserName) || s.FromUserName.Equals(user.UserName)));
            return PartialView();
        }

        /// <summary>
        /// 删除已读临时信息并添加至消息记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteMessage() {
            List<WX_Queue> list= new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.MsgState == 2);
            var isTrue = false; //是否删除
            if (list != null)
            {
                WX_CustomMsg mess = new WX_CustomMsg();
                SYS_User user = Session["User"] as SYS_User;
                foreach (WX_Queue item in list)
                {
                    mess.MsgId = item.MsgId;
                    mess.XmlContent = item.XmlContent;
                    mess.Content = item.XmlContent;
                    mess.UserId = user.UserId;
                    mess.CreateTime = item.CreateTime;
                    mess.MsgSource = "用户";
                    mess.AppId = item.AppId;
                    mess.MsgType = item.MsgType;
                    new WXDataBLL.WXCustom.WX_CustomMsgManger().Add(mess);
                    isTrue = new WXDataBLL.WXCustom.WX_QueueManager().Delete(item);
                }
            }
            return Json(isTrue,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 发送消息给用户
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TaleToUser(WX_CustomMsg msg)
        {
            bool IsTrue = new WXDataBLL.WXCustom.WX_CustomMsgManger().Add(msg);
            return Json(IsTrue, JsonRequestBehavior.AllowGet);
        }
    }
}