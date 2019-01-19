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
using Newtonsoft.Json;

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
                    x.UserId,
                })
                .FirstOrDefault();
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有本客服下的用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ActionResult GetAllUserInfo(int UserID)
        {
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
            var list = UserCode.OrderBy(x => x.Code).Select(x => x.Code).Distinct(); //获取当前所有不同分组，去掉重复的部分
            foreach (var item in list)
            {
                if (!item.Equals("#"))
                {
                    var UCode = UserCode.OrderBy(x => x.Code).Where(s => s.Code.Equals(item)).ToList();
                    Dic.Add(item, UCode);
                }
            }
            Dic.Add("#", UserCode.Where(x => x.Code.Equals("#")).ToList());
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
                Tag = x.WX_UserTag.Select(s => new
                {
                    s.TagId,
                    s.TagName
                }),
                x.UserName,
                x.UserNick,
                x.Province,
                x.City,
                x.Address,
                x.UserSex,
                x.HeadImageUrl,
                x.Telphone,
                x.Remark
            }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 按照客服编号查找所有有用户的标签
        /// </summary>
        /// <param name="UserId">客服编号</param>
        /// <returns></returns>
        public ActionResult UserOnTag(int UserId)
        {
            var info = new WX_UserTagManager().Where(s => s.WX_User.Where(x => x.UserId == UserId) != null).Select(s => new
            {
                //UserStr= JsonConvert.SerializeObject(s.WX_User.Select(x => new
                //{
                //    UserName = x.UserName,
                //    UserNick = x.UserNick,
                //    OpenID = x.OpenID,
                //    HeadImageUrl = x.HeadImageUrl,
                //}).ToList()),
                s.TagId,
                Count = s.WX_User.Count(),
                s.TagName,
            }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据标签编号和客服编号查询所有用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="TagId"></param>
        /// <returns></returns>
        public ActionResult TagInfo(int UserId, int TagId)
        {
            var info = new WX_UserTagManager().Where(x => x.TagId == TagId && x.WX_User.Where(s => s.UserId == UserId) != null).Select(s => new
            {
                User = s.WX_User.Select(x => new
                {
                    x.UserName,
                    x.UserNick,
                    x.HeadImageUrl,
                    x.OpenID
                }),
                s.TagName
            }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询用户分组
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult UserOnGroup(int UserID)
        {
            var info = new WX_UserGroupManager().Where(s => s.WX_User.Where(x => x.UserId == UserID && x.GroupId == x.WX_UserGroup.GroupId) != null).Select(s => new
            {
                GroupName = s.GroupName,
                Count = s.WX_User.Count(),
                GroupId = s.GroupId
            }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据客服编号和用户分组查询用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public ActionResult GroupInfo(int UserID, int GroupID)
        {
            var info = new WX_UserGroupManager().Where(s => s.WX_User.Where(x => x.UserId == UserID) != null && s.GroupId == GroupID).Select(x => new
            {
                x.GroupName,
                User = x.WX_User.Select(s => new
                {
                    s.HeadImageUrl,
                    s.OpenID,
                    s.UserNick,
                    s.UserName
                }).ToList()
            }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }
    }
}