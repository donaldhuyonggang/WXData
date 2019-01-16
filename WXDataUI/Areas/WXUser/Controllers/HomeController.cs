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


        public PageList<WX_User> GetUsers(int type,int pageIndex,int pageSize,int key)
        {
            WX_App app = (Session["SYSUSER"] as SYS_User).WX_App;
            List<WX_User> list;  
            if(type == 2)   //查询未分配用户
            {
                list = new WX_UserManager().Where(u => u.AppId == app.AppId && u.UserId == null);
            }else if(type == 3)//根据标签查询
            {
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
            user.GroupId = GroupId;
            var result = new ReturnResult()
            {
                ErrorMsg = "修改失败!",
                Result = bll.Update(user)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
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