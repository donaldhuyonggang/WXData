﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.Base;
using WXDataModel;
using WXDataUI.Models;
using WXService.Utility;

namespace WXDataUI.Areas.Base.Controllers
{
    public class APPController : Controller
    //public class APPController: Controller_EX
    {
        // GET: Base/APP
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ChangeApp()
        {
            ViewBag.AppList = new WX_AppManager().GetAll();
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeApp(string appId)
        {
            WX_App app = new WX_AppManager().GetByPK(appId);
            Session["App"] = app;
            Session["AppName"] = app.AppName;
            return Redirect("/Base/Home/Index");
        }

        [HttpPost]
        public ActionResult GetAppList(string type = "All", string key = null)
        {

            List<WX_App> list = null;
            if (type.Equals("All"))
            {
                list = new WX_AppManager().GetAll();
            }else
            {
                list = new WX_AppManager().Where(a => a.AppType.Equals(type));
            }
            if (!string.IsNullOrEmpty(key))
            {
                JObject jo = JObject.Parse(key);
                if (!string.IsNullOrEmpty(jo["AppName"].ToString()))
                {
                    string AppName = jo["AppName"].ToString();
                    list = list.Where(a => a.AppName.Contains(AppName)).ToList();
                }
                if (!string.IsNullOrEmpty(jo["WXId"].ToString()))
                {
                    string WXId = jo["WXId"].ToString();
                    list = list.Where(a => a.WXId.Contains(WXId)).ToList();
                }
                if (!string.IsNullOrEmpty(jo["CompanyName"].ToString()))
                {
                    string CompanyName = jo["CompanyName"].ToString();
                    list = list.Where(a => a.CompanyName != null && a.CompanyName.Contains(CompanyName)).ToList();
                }
            }

            var json = list.Select(s => new
            {
                s.AppType,
                s.AppName,
                s.AppId,
                s.WXId,
                s.CompanyName,
                s.AppState,
                s.Remark
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddApp()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddApp(WX_App app)
        {
            app.AppState = "正常";
            return Json(new WX_AppManager().Add(app), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult UpdateApp(string id)
        {
            ViewBag.App = new WX_AppManager().GetByPK(id);
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateApp(WX_App app)
        {
            return Json(new WX_AppManager().Update(app),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteApp(string id)
        {


           
            WX_AppManager bll = new WX_AppManager();
            WX_App app = bll.GetByPK(id);
            WX_App app1 = new WX_App();
            EntityUntility.CopyProperty(app, app1);
            app1.AppState = "无效"; 

            return Json(new WX_AppManager().Update(app1), JsonRequestBehavior.AllowGet);
        }

       
    }
}