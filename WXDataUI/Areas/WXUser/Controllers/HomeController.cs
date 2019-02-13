using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.WXCustom;
using WXDataBLL.WXUser;
using WXDataModel;
using WXDataUI.Helpers;
using WXDataUI.Models;
using WXService.Service;
using WXService.Utility;

namespace WXDataUI.Areas.WXUser.Controllers
{
    public class HomeController : Controller
    {
        // GET: WXUser/Home
        public ActionResult Index(int type = 1, int pageIndex = 1, int pageSize = 3,int key = 0)
        {
            string appId = (Session["SYSUSER"] as SYS_User).AppId;
            ViewBag.TagList = new WX_UserTagManager().GetTagList(appId);
            ViewBag.GroupList = new WX_UserGroupManager().GetGroupList(appId);
            ViewBag.Page = GetUsers(type, pageIndex, pageSize,key);
            return View();
        }

        //为用户分配客服
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
            WX_UserManager bll = new WX_UserManager();
            WX_User user = bll.GetByPK(OpenId);
            WX_User user1 = new WX_User();
            EntityUntility.CopyProperty(user, user1);
            user1.UserId = UserId;
            return Json(new WX_UserManager().Update(user1),JsonRequestBehavior.AllowGet);
        }
        //为用户分配客服end

        //从数据库获取用户
        public PageList<WX_User> GetUsers(int type,int pageIndex,int pageSize,int key)
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            List<WX_User> list;  
            if(type == 2)   //查询未分配用户
            {
                list = new WX_UserManager().Where(u => u.AppId == app.AppId && u.UserId == null);
            }else if(type == 3)//根据标签查询
            {
                if (key.Equals(-1))
                    list = app.WX_User.Where(u => u.WX_UserTag.Count == 0).ToList();
                else
                    list = new WX_UserTagManager().GetByPK(key).WX_User.Where(u => u.AppId == app.AppId).ToList();
            }
            else if (type == 4)//根据分组查询
            {
                list = new WX_UserGroupManager().GetByPK(key).WX_User.Where(u => u.AppId == app.AppId).ToList();
            }
            else
            {
                list = new WX_UserManager().Where(u => u.AppId == app.AppId);
            }
            PageList<WX_User> page = new PageList<WX_User>(list,pageIndex,pageSize); 
            return page;

        }

        //查看用户详情
        public ActionResult UserInfo(string id)
        {
            ViewBag.User = new WX_UserManager().GetByPK(id);
            return PartialView();
        }

        //修改用户分组
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
            user.GroupId = GroupId;
            var result = new ReturnResult()
            {
                ErrorMsg = "修改失败!",
                Result = bll.Update(user)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //修改用户分组end


        private void GetByWX()
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            if (app != null)
            {

            }
        }

        //为用户添加标签
        [HttpGet]
        public ActionResult AddTag(string openId)
        {
            ViewBag.openId = openId;
            ViewBag.TagList = new WX_UserTagManager().Where(t => t.AppId.Equals((Session["SYSUSER"] as SYS_User).WX_App.AppId));
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddTag(List<string>openId,int tagid)
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            JObject jo = JObject.Parse(new UserService(app.AppId, app.AppSecret).AddTag(openId, tagid));
            var result = new
            {
                errcode = jo["errcode"].ToString(),
                errmsg = jo["errmsg"].ToString()
            };
            if(result.errcode.Equals("0"))
            {
                WX_UserManager manager = new WX_UserManager();
                foreach (var id in openId)
                {
                    WX_User user = manager.GetByPK(id);
                    WX_User user1 = new WX_User();
                    EntityUntility.CopyProperty(user, user1);
                    user1.WX_UserTag.Add(new WX_UserTag() {TagId = tagid });
                    manager.Update(user1);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //为用户添加标签end

        /// <summary>
        /// 从服务器更新用户列表
        /// </summary>
        public ActionResult UpdateList()
        {
            ReturnResult rs = new ReturnResult();
            WX_UserManager manager = new WX_UserManager();
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            UserService ser = new UserService(app.AppId, app.AppSecret);
            try
            {
                
                JToken jo = JObject.Parse(ser.Get());
                var list = new List<WX_User>();
                foreach (string i in jo["data"]["openid"].Children())
                {
                    string json = ser.Info(i);
                    JObject userJo = JObject.Parse(json);
                    if (userJo["subscribe"].ToString().Equals("0"))//已退订
                    {
                        manager.Update(new WX_User() { OpenID = i, UnSubscribeTime = DateTime.Now, UserState = "已退订" });
                    }
                    else
                    {
                        long unixTimeStamp = Convert.ToInt32(userJo["subscribe_time"]);
                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                        DateTime dt = startTime.AddSeconds(unixTimeStamp);
                        System.Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss:ffff"));
                        WX_User user = new WX_User()
                        {
                            OpenID = i,
                            AppId = app.AppId,
                            UserNick = userJo["nickname"].ToString(),
                            UserSex = userJo["sex"].ToString().Equals("1") ? "男" : "女",
                            City = userJo["city"].ToString(),
                            Province = userJo["province"].ToString(),
                            Country = userJo["country"].ToString(),
                            HeadImageUrl = userJo["headimgurl"].ToString(),
                            SubscribeTime = dt,
                            Remark = userJo["remark"].ToString(),
                            GroupId = Convert.ToInt32(userJo["groupid"]),
                            UserState = "正常",
                        };
                        foreach (var t in userJo["tagid_list"].Children())
                        {
                            user.WX_UserTag.Add(new WX_UserTagManager().GetByPK(Convert.ToInt32(t)));
                        }
                        WX_User info = manager.GetByPK(user.OpenID);
                        if (info == null)//新增
                        {
                            manager.Add(user);
                        }
                        else
                        {
                            manager.Update(user);
                        }
                    }
                }

            }
            catch (Exception)
            {
                rs.Result = false;
                rs.ErrorMsg = "更新失败!";
                return Json(rs,JsonRequestBehavior.AllowGet);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}