using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataBLL.WXMenu;
using WXDataModel;
using WXDataModel.Extend;
using WXDataUI.Filter;

namespace WXDataUI.Areas.WXMenu.Controllers
{

    [RightFilter(Message = "菜单事件管理")]
    public class EventController : Controller
    {
        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }
        // GET: WXMedia/Event
        public ActionResult Index()
        {
            ViewBag.DataList = new WX_MenuEventManager().Where(m => m.AppId.Equals(WXAPP.AppId));
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.MenuList = new WX_MenuManager().Where(m => m.AppId.Equals(WXAPP.AppId) && m.ParentMenuId != null && !m.IsHasEvent());
            ViewBag.NewsList = new WX_MediaManager().Where(m => m.AppId.Equals(WXAPP.AppId) && m.MediaType.Equals("news")).TransitionToNews();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(WX_MenuEvent me)
        {
            me.AppId = WXAPP.AppId;
            var r = new WX_MenuEventManager().Add(me);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string key)
        {
            var r = new WX_MenuEventManager().Delete(new object[]{ WXAPP.AppId, key });
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}