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
using Newtonsoft.Json.Linq;
using WXService.Service;

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
                x.OpenID,
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
        /// 修改指定用户个人信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UpUserIDL(WX_User user) {
            var info = new WX_UserManager().NewUpdate(user);
            return Json(info,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 按照客服编号查找所有有用户的标签
        /// </summary>
        /// <param name="UserId">客服编号</param>
        /// <returns></returns>
        public ActionResult UserOnTag(int UserId,string AppId)
            {
            var info = new WX_UserTagManager().Where(s => s.WX_User.Where(x => x.UserId == UserId) != null && s.AppId==AppId).Select(s => new
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
        public ActionResult TagInfo(int UserId, int TagId,string AppID)
        {
            var info = new WX_UserTagManager().Where(x => x.TagId == TagId && x.WX_User.Where(s => s.UserId == UserId) != null && x.AppId == AppID).Select(s => new
            {
                User = s.WX_User.Select(x => new
                {
                    x.UserName,
                    x.UserNick,
                    x.HeadImageUrl,
                    x.OpenID
                }),
                s.TagName,
                s.TagId,
                s.AppId
            }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 从数据库查找标签是否存在
        /// </summary>
        /// <returns></returns>
        public ActionResult FindTag(string TagName,string AppId) {
            var info = new WX_UserTagManager().Where(x => x.TagName == TagName && x.AppId == AppId).Count() > 0;
            return Json(info,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 从微信删除标签以及该标签下的所有用户
        /// </summary>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public ActionResult DeleteTag(int TagId, string AppId)
        {
            WX_App app = new WX_AppManager().GetByPK(AppId);
            TagService ser = new TagService(app.AppId, app.AppSecret);
            JObject jo = JObject.Parse(ser.Delete(TagId));
            var result = new
            {
                errcode = jo["errcode"].ToString(),
                errmsg = jo["errmsg"].ToString()
            };
            if (result.errcode.Equals("0"))
            {
                new WX_UserTagManager().Delete(TagId, app.AppId);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增标签到微信并在此标签下新增用户
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public ActionResult AddTagAndUse(string AppId, string tagName,string OpenId)
        {
            WX_App app = new WX_AppManager().GetByPK(AppId);
            TagService ser = new TagService(app.AppId, app.AppSecret);
            string json = ser.Create(tagName);
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            WX_UserTag tag = new WX_UserTag()
            {
                AppId = AppId,
                TagId = (int)jo["tag"]["id"],
                TagName = jo["tag"]["name"].ToString(),
            };
            ReturnResult result = new ReturnResult() { Result = true };
            if (!new WX_UserTagManager().Add(tag))
            {
                result.Result = false;
                result.ErrorMsg = "新增失败!";
            }
            else
            {
                List<string> openIdList = JsonConvert.DeserializeObject<List<string>>(OpenId);
                JObject job = JObject.Parse(new UserService(app.AppId, app.AppSecret).AddTag(openIdList, tag.TagId));
                var Adresult = new
                {
                    errcode = job["errcode"].ToString(),
                    errmsg = job["errmsg"].ToString()
                };
                if (Adresult.errcode.Equals("0"))
                {
                    WX_UserManager manager = new WX_UserManager();
                    foreach (var id in openIdList)
                    {
                        manager.AddTag(manager.GetByPK(id), tag.TagId);
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 删除该标签下的用户
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoveTag(string OpenID, int tagId,string AppId)
        {
            var Ap = new WX_AppManager().GetByPK(AppId);
            JObject jo = JObject.Parse(new UserService(Ap.AppId, Ap.AppSecret).RemoveTag(OpenID, tagId));
            var result = new
            {
                errcode = jo["errcode"].ToString(),
                errmsg = jo["errmsg"].ToString()
            };
            if (result.errcode.Equals("0"))
            {
                WX_UserManager manager = new WX_UserManager();
                manager.RemoveTag(manager.GetByPK(OpenID), tagId);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 给用户添加标签
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public ActionResult AddTag(string OpenId, string AppId,int tagid=0)
        {
            List<string> openIdList = JsonConvert.DeserializeObject<List<string>>(OpenId);

            var Ap = new WX_AppManager().GetByPK(AppId);
            JObject jo = JObject.Parse(new UserService(Ap.AppId, Ap.AppSecret).AddTag(openIdList, tagid));
            var result = new
            {
                errcode = jo["errcode"].ToString(),
                errmsg = jo["errmsg"].ToString()
            };
            if (result.errcode.Equals("0"))
            {
                WX_UserManager manager = new WX_UserManager();
                foreach (var id in openIdList)
                {
                    manager.AddTag(manager.GetByPK(id), tagid);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑该标签名
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ActionResult EditTag(WX_UserTag tag)
        {
            var Ap = new WX_AppManager().GetByPK(tag.AppId);
            TagService ser = new TagService(Ap.AppId, Ap.AppSecret);
            JObject jo = JObject.Parse(ser.Update(tag.TagId, tag.TagName));
            var result = new
            {
                errcode = jo["errcode"],
                errmsg = jo["errmsg"]
            };
            GetTagList(Ap.AppId,Ap.AppSecret);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 从服务器更新标签集合
        /// </summary>
        /// <returns></returns>
        public List<WX_UserTag> GetTagList(string AppID,string Secret)
        {
            WX_UserTagManager manager = new WX_UserTagManager();
            TagService ser = new TagService(AppID, Secret);
            List<WX_UserTag> list = new List<WX_UserTag>();
            JToken jo = JObject.Parse(ser.GetList())["tags"];
            foreach (var i in jo.Children())
            {
                var tag = new WX_UserTag()
                {
                    TagId = (int)i["id"],
                    TagName = i["name"].ToString(),
                    AppId = AppID
                };
                var info = manager.GetAll().Where(t => t.TagId == Convert.ToInt32(i["id"]) && t.AppId.Equals(AppID));
                if (info.Count() > 0)
                {
                    //info.TagName = tag.TagName;
                    manager.Update(tag);
                }
                else
                {
                    manager.Add(tag);
                }
                list.Add(tag);
            }

            var idList = new List<int>();
            foreach (var i in manager.GetAll())
            {
                if (list.Where(t => t.TagId.Equals(i.TagId)).Count() == 0)
                {
                    idList.Add(i.TagId);
                }
            }
            foreach (var id in idList)
            {
                manager.Delete(id, AppID);
            }

            Controller_EX.BindSession(Session);
            return list;
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


        /// <summary>
        /// 查询未分配客服的用户
        /// </summary>
        /// <returns></returns>
        public ActionResult NewF(string AppId) {
            var info = new WX_UserManager().Where(s => s.AppId == AppId && s.UserId == null).Select(x => new
            {
                x.UserNick,
                x.OpenID,
                x.HeadImageUrl,
                x.City,
                x.Country
            });
            return Json(info,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 分配客服
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult UpNewF(string OpenId,int UserId) {
            var info = new WX_UserManager().UpUserId(OpenId,UserId);
            return Json(info,JsonRequestBehavior.AllowGet);
        }
    }
}