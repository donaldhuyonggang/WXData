using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataModel;
using WXDataUI.Models;

namespace WXDataUI.Areas.Base.Controllers
{
    /// <summary>
    /// 手机App控制器
    /// </summary>
    public class MobileController : Controller
    {
        // GET: Base/Mobile
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult Login(SYS_User info)
        {
            var user = new SYS_UserManager().Where(s => s.LoginId == info.LoginId && s.Password == info.Password)
                .Select(
                x => new
                {
                    x.LoginId,
                    x.Password,
                    x.UserName,
                    x.WXId,
                    x.AppId,
                    x.RoleId,
                    x.HeadImageUrl,
                    x.UserId
                })
                .FirstOrDefault();
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当前登陆用户下的公众号
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult GetWeChat(int UserId)
        {
            var info = new SYS_UserManager().Where(s => s.UserId == UserId&&s.WX_App.AppState=="正常")
                .Select(x => new
                {
                    AppId=x.WX_App.AppId,
                    AppName=x.WX_App.AppName,
                    AppSecret=x.WX_App.AppSecret,
                    WXId=x.WX_App.WXId,
                    AppType=x.WX_App.AppType
                })
                .ToList();
            return Json(info,JsonRequestBehavior.AllowGet);
        }
    }
}