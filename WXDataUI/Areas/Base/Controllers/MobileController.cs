using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataModel;
using WXDataBLL.WXUser;
using WXDataUI.Models;
using WXService.Utility;
using System.Collections;

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
        /// 获取所有本客服下的用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ActionResult GetAllUserInfo(int UserID) {
            List<WX_User> UserInfo = new WX_UserManager().Where(s => s.UserId == UserID).ToList(); //获取所有该客服下的用户
            List<GetCharSpellCode> UserCode = new List<GetCharSpellCode>(); //存储所有用户以及首字母
            Dictionary<string, List<GetCharSpellCode>> Dic = new Dictionary<string, List<GetCharSpellCode>>(); //将用户以键值对的方式存储
            foreach (WX_User item in UserInfo)
            {
                GetCharSpellCode code = new GetCharSpellCode();
                if (item.UserName != null) //判断备注是否为空
                {
                    code.Code = GetInitialUtility.GetFirstPinYin(item.UserName);
                }
                else
                {
                    code.Code = GetInitialUtility.GetFirstPinYin(item.UserNick);
                }
                code.UserName = item.UserName;
                code.UserNick = item.UserNick;
                code.OpenID = item.OpenID;
                code.HeadImageUrl = item.HeadImageUrl;
                UserCode.Add(code);//添加到集合
            }
            var list = UserCode.OrderBy(x=>x.Code).Select(x=>x.Code).Distinct(); //获取当前所有不同分组，去掉重复的部分
            foreach (var item in list)
            {
                if (!item.Equals("#"))
                {
                    var UCode = UserCode.OrderBy(x => x.Code).Where(s => s.Code.Equals(item)).ToList();
                    Dic.Add(item, UCode);
                }
            }
            Dic.Add("#",UserCode.Where(x=>x.Code.Equals("#")).ToList());
            return Json(Dic.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当前登陆用户下的公众号
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult GetWeChat(int UserId)
        {
            var info = new SYS_UserManager().Where(s => s.UserId == UserId && s.WX_App.AppState == "正常")
                .Select(x => new
                {
                    AppId = x.WX_App.AppId,
                    AppName = x.WX_App.AppName,
                    AppSecret = x.WX_App.AppSecret,
                    WXId = x.WX_App.WXId,
                    AppType = x.WX_App.WX_AppType.TypeName
                })
                .ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询指定用户个人信息
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public ActionResult UserIDL(string OpenID)
        {
            var info = new WX_UserManager().Where(s => s.OpenID.Equals(OpenID)).Select(x => new
            {
                UserName = x.UserName,
                UserNick = x.UserNick,
                Province = x.Province,
                City = x.City,
                Address = x.Address,
                UserSex = x.UserSex,
                HeadImageUrl = x.HeadImageUrl
            }).FirstOrDefault();
            return Json(info, JsonRequestBehavior.AllowGet);
        }
    }
}