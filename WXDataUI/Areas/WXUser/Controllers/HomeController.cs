using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.WXCustom;
using WXDataBLL.WXUser;
using WXDataModel;

namespace WXDataUI.Areas.WXUser.Controllers
{
    public class HomeController : Controller
    {
        // GET: WXUser/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AllotUser(string id)
        {
            ViewBag.SYSUserList = (Session["SYSUSER"] as SYS_User).WX_App.SYS_User.ToList();

            ViewBag.OpenId = id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult AllotUser(string OpenId,int UserId)
        {
            BaseBLL<WX_User> bll = new WX_UserManager();
            WX_User user = bll.GetByPK(OpenId);
            WX_User user1 = new WX_User();
            
            user.UserId = UserId;
            if (new WX_UserManager().Update(user))
            {
                return Redirect("/WXUser/Home/Index");
            }
            return Content("false");
        }


        public ActionResult GetUsers(int type = 1)
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            List<WX_User> list;  
            if (type != 1)
            {
                list = new WX_UserManager().Where(u => u.AppId == app.AppId && u.UserId == null);
            }else
            {
                list = new WX_UserManager().Where(u => u.AppId == app.AppId);
            }
            var json = list.Select(u => new {
                FollowStatus = (u.UnSubscribeTime == null?"已关注":"已退订"),
                u.OpenID,
                u.UserNick,
                u.HeadImageUrl,
                u.UserSex,
                u.City,
                u.Province,
                u.Country,
                SubscribeTime = u.SubscribeTime.ToString(),
                SYS_UserName = (u.UserId==null?"暂无所属客服":u.SYS_User.UserName),
            });
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UserInfo(string id)
        {
            ViewBag.User = new WX_UserManager().GetByPK(id);
            return PartialView();
        }

        [HttpGet]
        public ActionResult ChangeGroup(string id)
        {
            ViewBag.GroupList = new WX_UserGroupManager().GetAll();
            ViewBag.User = new WX_UserManager().GetByPK(id);
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeGroup(string OpenId,int GroupId)
        {
            WX_UserManager bll = new WX_UserManager();
            WX_User user = bll.GetByPK(OpenId);
            user.GrooupId = GroupId;
            if (bll.Update(user))
            {
                return Redirect("/WXUser/Home/Index");
            }
            return Content("false");
        }
        private void GetByWX()
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            if (app != null)
            {

            }
        }
    }
}