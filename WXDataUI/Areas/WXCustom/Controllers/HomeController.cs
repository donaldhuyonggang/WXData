using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataModel;
using WXService.Service;
using WXService.Utility;

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
            var list = new WXDataBLL.WXCustom.WX_UserManager().Where(s => s.UserId == user.UserId || s.UserId.Equals("")).ToList();
            var Data = list.Select(s => new
            {
                id = s.OpenID,
                text = s.UserNick
            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo(string id)
        {
            //通过主键获取
            SYS_User user = Session["User"] as SYS_User;
            WX_User Wuser = new WXDataBLL.WXCustom.WX_UserManager().GetByPK(id);
            Wuser.UserId = user.UserId;
            return PartialView();
        }

        //public ActionResult UpuserID(string id)
        //{
        //    //修改
        //    var isTrue = new WXDataBLL.WXCustom.WX_UserManager().Update();
        //    ViewBag.user = Wuser;
        //}

        /// <summary>
        /// 查看当前用户聊天
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TaleToUser(string id)
        {
            ViewBag.OpenId = id;
            ViewBag.Talk = FansMsg(id);
            return PartialView();
        }

        /// <summary>
        /// 聊天内容输出到网页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<WX_CustomMsg> FansMsg(string id)
        {
            SYS_User user = Session["User"] as SYS_User;
            List<WX_Queue> list = new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.MsgState == 1 && s.OpenID.Equals(id));
            List<WX_CustomMsg> msg = new List<WX_CustomMsg>();
            if (list != null)
            {
                foreach (WX_Queue item in list)
                {
                    WX_CustomMsg CM = new WX_CustomMsg();
                    CM.MsgId = item.MsgId;
                    CM.OpenID = item.OpenID;
                    CM.UserId = user.UserId;
                    CM.AppId = user.AppId;
                    CM.CreateTime = item.CreateTime;
                    CM.Content = XmlUtility.GetSingleNodeInnerText(item.XmlContent, "/xml/Content");
                    CM.MsgSource = "粉丝";
                    CM.MsgType = item.MsgType;
                    CM.XmlContent = item.XmlContent;
                    new WXDataBLL.WXCustom.WX_CustomMsgManger().Add(CM); //添加到数据库
                    msg.Add(CM);//添加到集合
                    new WXDataBLL.WXCustom.WX_QueueManager().Delete(item.MsgId);//删除
                }
            }
            return msg;
        }

        /// <summary>
        /// 发送消息给用户
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TaleToUser(WX_CustomMsg msg)
        {
            SYS_User user = Session["User"] as SYS_User;
            msg.UserId = user.UserId;
            msg.MsgId = Guid.NewGuid().ToString();
            msg.CreateTime = DateTime.Now;
            msg.MsgSource = "客服";

            //发送到微信
            CustomService customSvr = new CustomService(user.AppId, user.WX_App.AppSecret);
            customSvr.SendText(msg.OpenID, msg.Content);

            bool IsTrue = new WXDataBLL.WXCustom.WX_CustomMsgManger().Add(msg);
            return Json(IsTrue, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 接收粉丝发送的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetFansMsg(string id)
        {
            List<WX_CustomMsg> Que = FansMsg(id);
            return Json(Que, JsonRequestBehavior.AllowGet);
        }
    }
}