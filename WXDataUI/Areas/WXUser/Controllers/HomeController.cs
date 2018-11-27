using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
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

        public ActionResult GetUsers(int type = 1)
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            List<WX_User> list;  
            if (type != 1)
            {
                list = new BaseBLL<WX_User>().Where(u => u.AppId == app.AppId && u.UserId == null);
            }else
            {
                list = new BaseBLL<WX_User>().Where(u => u.AppId == app.AppId);
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
                u.SubscribeTime,
                SYS_UserName = (u.UserId==null?"暂无所属客服":u.SYS_User.UserName)
            });
            return Json(json, JsonRequestBehavior.AllowGet);

        }
    }
}