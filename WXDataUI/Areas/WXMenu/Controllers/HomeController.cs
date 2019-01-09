using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataModel;

namespace WXDataUI.Areas.WXMenu.Controllers
{
    public class HomeController : Controller
    {
        // GET: WXMenu/Home
        public ActionResult Index()
        {
            if (Session["SYSUSER"] == null)
            {
                return Redirect("/base/home/login");
            }
            else
            {
                string id = (Session["SYSUSER"] as SYS_User).AppId;
                var list2 = new WXDataBLL.WXMenu.WXMenuManger().Where(g => g.AppId == id && g.ParentMenuId == null && g.MenuVisable == 0).ToList();//获取当前公众号的所有一级菜单
                List<WX_Menu> list3 = new List<WX_Menu>();//二级菜单
                foreach (var item in list2)
                {
                    var data = new WXDataBLL.WXMenu.WXMenuManger().Where(g => g.AppId == id && g.ParentMenuId == item.MenuId && g.MenuVisable == 0).ToList();
                    foreach (var item2 in data)
                    {
                        list3.Add(item2);
                    }
                }
                ViewBag.yiji = list2;
                ViewBag.erji = list3;
                return View();
            }
        }

        /// <summary>
        /// 根据公众号id获取所有菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Select_gzh_id()
        {
            if (Session["SYSUSER"] == null)
            {
                return Redirect("/base/home/login");
            }
            string id = (Session["SYSUSER"] as SYS_User).AppId;
            var list1 = new WXDataBLL.WXMenu.WXMenuManger().Where(g => g.AppId == id && g.MenuVisable == 0).ToList();
            var json = list1.Select(s => new
            {
                s.MenuId,
                s.AppId,
                s.MenuName,
                s.MenuType,
                s.MenuKey,
                s.MenuUrl,
                s.MenuVisable,
                s.MenuSort,
                s.ParentMenuId
            });
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据id逻辑删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delect_id(int id)
        {
            bool pd = new WXDataBLL.WXMenu.WXMenuManger().Delect_id(id);
            return Json(pd, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增一级菜单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add() {
            if (Session["SYSUSER"] == null)
            {
                return Redirect("/base/home/login");
            }
            bool pd = new WXDataBLL.WXMenu.WXMenuManger().Add_yiji_pd((Session["SYSUSER"] as SYS_User).AppId);
            ViewBag.pd = pd;
            return View();
        }















        /// <summary>
        /// 模态框 判断新建子级元素判断二级菜单是否超过五个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add_zi(int id)
        {
            bool pd = new WXDataBLL.WXMenu.WXMenuManger().Add_zi_pd(id);
            ViewBag.pd = pd;
            return PartialView();
        }

        /// <summary>
        /// 新增一级菜单  判断当前公众号的一级菜单是否超过5个
        /// </summary>
        /// <returns></returns>
        public ActionResult Add_yiji_pd()
        {
            if (Session["SYSUSER"] == null)
            {
                return Redirect("/base/home/login");
            }
            bool pd = new WXDataBLL.WXMenu.WXMenuManger().Add_yiji_pd((Session["SYSUSER"] as SYS_User).AppId);
            return Json(pd, JsonRequestBehavior.AllowGet);
        }


    }
}