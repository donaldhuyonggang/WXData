using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.WXMenu;
using WXDataModel;
using WXDataModel.Extend;
using WXService.Service;

namespace WXDataUI.Areas.WXMenu.Controllers
{
    public class HomeController : Controller
    {
        public WX_App WXAPP
        {
            get
            {
                return (Session["SYSUSER"] as SYS_User).WX_App;
            }
        }
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
                var list2 = new WX_MenuManager().Where(g => g.AppId == id && g.ParentMenuId == null && g.MenuVisable == 0).ToList();//获取当前公众号的所有一级菜单
                List<WX_Menu> list3 = new List<WX_Menu>();//二级菜单
                foreach (var item in list2)
                {
                    var data = new WX_MenuManager().Where(g => g.AppId == id && g.ParentMenuId == item.MenuId && g.MenuVisable == 0).ToList();
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
            var list1 = new WX_MenuManager().Where(g => g.AppId == id && g.MenuVisable == 0).ToList();
            var json = list1.Select(s => new
            {
                s.MenuId,
                s.AppId,
                s.MenuName,
                s.WX_MenuType.TypeName,
                s.MenuKey,
                s.MenuUrl,
                s.MenuVisable,
                s.MenuSort,
                s.ParentMenuId
            });
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //获取所有一级菜单
        public ActionResult GetParentMenu()
        {
            var list = new WX_MenuManager().Where(g => g.AppId == WXAPP.AppId && g.ParentMenuId == null && g.MenuVisable == 0 && g.WX_Menu1.Count() < 5).Select(m => new
            {
                m.MenuId,
                m.MenuName
            }).ToList();//获取当前公众号的所有一级菜单
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据id逻辑删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            bool pd = new WX_MenuManager().Delect_id(id);
            return Json(pd, JsonRequestBehavior.AllowGet);
        }
        //更新菜单
        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.MenuType = new WX_MenuTypeManager().GetAll();
            ViewBag.PList = new WX_MenuManager().Where(g => g.AppId == WXAPP.AppId && g.ParentMenuId == null && g.MenuVisable == 0 && g.WX_Menu1.Count() < 5).ToList();//获取当前公众号的所有一级菜单
            ViewBag.Menu = new WX_MenuManager().GetByPK(id);
            return PartialView();
        }
        public ActionResult Update(WX_Menu info)
        {
            info.AppId = WXAPP.AppId;
            var r = new WX_MenuManager().Update(info);
            return Json(r,JsonRequestBehavior.AllowGet);
        }
        //更新菜单end

        [HttpGet]
        /// <summary>
        /// 新增一级菜单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMenu() {
            bool pd = new WX_MenuManager().Add_yiji_pd((Session["SYSUSER"] as SYS_User).AppId);
            ViewBag.MenuType = new WX_MenuTypeManager().GetAll();
            ViewBag.pd = pd;
            ViewBag.PList = new WX_MenuManager().Where(g => g.AppId == WXAPP.AppId && g.ParentMenuId == null && g.MenuVisable == 0 && g.WX_Menu1.Count() < 5).ToList();//获取当前公众号的所有一级菜单
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddMenu(WX_Menu menu)
        {
            menu.AppId = WXAPP.AppId;
            menu.MenuVisable = 0;
            var result = new WX_MenuManager().Add(menu);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //同步至服务器
        public ActionResult Sync()
        {
            List<WX_Menu> list = new WX_MenuManager().Where(g => g.AppId == WXAPP.AppId && g.ParentMenuId == null && g.MenuVisable == 0).ToList();
            return Json(new MenuService(WXAPP.AppId,WXAPP.AppSecret).Create(list.GetJson()),JsonRequestBehavior.AllowGet);
        }

        //从服务器拉取菜单
        public void Pull()
        {
            var str = new MenuService(WXAPP.AppId, WXAPP.AppSecret).Get();
            var json = JObject.Parse(str)["menu"]["button"];
            WX_MenuManager manager = new WX_MenuManager();
            manager.Clear(WXAPP.AppId);
            foreach (var i in json.Children())
            {
                var menu = new WX_Menu();
                menu.AppId = WXAPP.AppId;
                menu.MenuName = i["name"].ToString();
                menu.MenuVisable = 0;
                if (i["sub_button"].Count() > 0)
                {
                    foreach (var j in i["sub_button"])
                    {
                        var sub_menu = new WX_Menu();
                        sub_menu.AppId = WXAPP.AppId;
                        sub_menu.MenuName = j["name"].ToString();
                        sub_menu.MenuVisable = 0;
                        string type = j["type"].ToString();
                        if (type.Equals("click"))
                        {
                            sub_menu.MenuKey = j["key"].ToString();
                            sub_menu.TypeId = 1;
                        }
                        else
                        {
                            sub_menu.MenuUrl = j["url"].ToString();
                            sub_menu.TypeId = 2;
                        }
                        menu.TypeId = 1;
                        menu.WX_Menu1.Add(sub_menu);
                    }
                }
                else
                {
                    string type = i["type"].ToString();
                    if (type.Equals("click"))
                    {
                        menu.MenuKey = i["key"].ToString();
                        menu.TypeId = 1;
                    }
                    else
                    {
                        menu.MenuUrl = i["url"].ToString();
                        menu.TypeId = 2;
                    }
                }

                manager.Add(menu);
            }
        }








        /// <summary>
        /// 模态框 判断新建子级元素判断二级菜单是否超过五个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add_zi(int id)
        {
            bool pd = new WX_MenuManager().Add_zi_pd(id);
            ViewBag.pd = pd;
            return PartialView();
        }

        /// <summary>
        /// 新增一级菜单  判断当前公众号的一级菜单是否超过3个
        /// </summary>
        /// <returns></returns>
        public ActionResult Add_yiji_pd()
        {
            if (Session["SYSUSER"] == null)
            {
                return Redirect("/base/home/login");
            }
            bool pd = new WX_MenuManager().Add_yiji_pd((Session["SYSUSER"] as SYS_User).AppId);
            return Json(pd, JsonRequestBehavior.AllowGet);
        }


        public ActionResult chaxun_sum() {
            string id = (Session["SYSUSER"] as SYS_User).AppId;
            var list1 = new WX_MenuManager().Where(g => g.AppId == id && g.MenuVisable == 0).ToList();
            var json = list1.Select(s => new
            {
                s.MenuId,
                s.AppId,
                s.MenuName,
                s.WX_MenuType.TypeName,
                s.MenuKey,
                s.MenuUrl,
                s.MenuVisable,
                s.MenuSort,
                s.ParentMenuId
            });
            return Json(json, JsonRequestBehavior.AllowGet);
        }


    }
}