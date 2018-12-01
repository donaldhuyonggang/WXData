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
            if (Session["SYSUSER"] == null)
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
        public ActionResult CreatTree()
        {
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            ViewBag.treenode= new WXDataBLL.WXUser.WX_UserGroupManager().Where(x=> ( x.AppId==null) || (x.AppId==SYSUSER.AppId && x.UserId==null) || (x.UserId== SYSUSER.UserId)).ToList();
            return PartialView();
        }

        /// <summary>
        /// 获取未读信息条数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCountMessage() {
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            if (SYSUSER!=null)
            {
                var list = new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.AppId == SYSUSER.AppId).ToList();
                var list1 = new WXDataBLL.WXUser.WX_UserManager().Where(s=>s.GrooupId==1).Where(x=> (x.AppId==SYSUSER.AppId && x.UserId==SYSUSER.UserId) || x.UserId==null).Select(x=>new { x.UserId,x.HeadImageUrl,x.OpenID,x.UserNick}).ToList();

                var data = list.GroupBy(s => s.OpenID).Select(s => new
                {
                    Count = s.Count(),
                    OpenID = s.Key
                }).ToList();

                var info = new
                {
                    CountList = data,
                    NewList = list1

                };

                return Json(info, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Redirect("/base/home/login");
            }
            
        }

        /// <summary>
        /// 用户个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo(string id)
        {
            ViewBag.user = new WXDataBLL.WXUser.WX_UserManager().GetByPK(id);
            return PartialView();
        }

        /// <summary>
        /// 跳转到历史记录页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserHistory(string id) {
            ViewBag.Openid = id;
            return PartialView();
        }

        /// <summary>
        /// 查看用户历史记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserHistory(string id, int page = 3, int rows = 1) {
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            var list = new WXDataBLL.WXCustom.WX_CustomMsgManger().Where(s => s.AppId == SYSUSER.AppId && s.UserId == SYSUSER.UserId && s.OpenID == id).OrderBy(x=>x.CreateTime).ToList();
            var Data = list.Skip((page - 1) * rows).Take(rows).Select(s => new
            {
                s.AppId,
                s.CreateTime,
                s.Content,
                s.WX_User.UserNick
            });
            var pageData = new
            {
                total = list.Count,
                rows = Data
            };
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 分配用户给客服，随抢
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpuserID(string id)
        {
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            WX_User WXUSER= new WXDataBLL.WXUser.WX_UserManager().GetByPK(id);
            var isTrue = false;
            if (WXUSER.UserId==null)
            {
                WXUSER.UserId = SYSUSER.UserId;
                //修改
                isTrue = new WXDataBLL.WXUser.WX_UserManager().Update(WXUSER);
            }
            return Json(isTrue, JsonRequestBehavior.AllowGet);
        }

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
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            List<WX_Queue> list = new WXDataBLL.WXCustom.WX_QueueManager().Where(s => s.MsgState == 1 && s.OpenID.Equals(id));
            List<WX_CustomMsg> msg = new List<WX_CustomMsg>();
            foreach (WX_Queue item in list)
            {
                WX_CustomMsg CM = new WX_CustomMsg();
                CM.MsgId = item.MsgId;
                CM.OpenID = item.OpenID;
                CM.UserId = SYSUSER.UserId;
                CM.AppId = SYSUSER.AppId;
                CM.CreateTime = item.CreateTime;
                CM.Content = XmlUtility.GetSingleNodeInnerText(item.XmlContent, "/xml/Content");
                CM.MsgSource = "粉丝";
                CM.MsgType = item.MsgType;
                CM.XmlContent = item.XmlContent;

                new WXDataBLL.WXCustom.WX_QueueManager().Delete(item.MsgId);//删除
                new WXDataBLL.WXCustom.WX_CustomMsgManger().Add(CM); //添加到数据库
                var info = new WXDataBLL.WXCustom.WX_CustomMsgManger().GetByPK(CM.MsgId);
                msg.Add(info);//添加到集合
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
            SYS_User SYSUSER = Session["SYSUSER"] as SYS_User;
            msg.UserId = SYSUSER.UserId;
            msg.MsgId = Guid.NewGuid().ToString();
            msg.CreateTime = DateTime.Now;
            msg.MsgSource = "客服";
            msg.AppId = SYSUSER.AppId;

            //发送到微信
            CustomService customSvr = new CustomService(SYSUSER.AppId, SYSUSER.WX_App.AppSecret);
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

            var Que1 = Que.Select(x => new
            {
                x.AppId,
                x.Content,
                x.WX_User.HeadImageUrl,
                x.WX_User.UserNick
            });
            return Json(Que1, JsonRequestBehavior.AllowGet);
        }
    }
}