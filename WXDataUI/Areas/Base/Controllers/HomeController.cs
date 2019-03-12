using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.Base;
using WXDataModel;
using WXDataModel.Extend;
using WXDataUI.Filter;
using WXDataUI.Helpers;
using WXDataUI.Models;
using WXService.Service;

namespace WXDataUI.Areas.Base.Controllers
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
        public HomeController()
        {

        }
        // GET: Base/Home
        [LoginFilter(Message = "Action")]
        public ActionResult Index()
        {
            if (Session["SYSUSER"] != null)
            {
                return View();
            }
            else
                return Redirect("Login");
        }

        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SYS_User info)
        {
            
            SYS_User user = new SYS_UserManager().Login(info);
            if (user!=null)
            {
                Session["SYSUSER"] = user;
                Controller_EX.BindSession(Session);
                return Json(true, JsonRequestBehavior.AllowGet);
                //return Redirect("/Base/Home/Index");
            }
            //return JavaScript("<script>alert('登录失败!');location.href='/Base/Home/Login'</script>");
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            
            Session.Clear();
            
            return Redirect("/Base/Home/Login");
        }

        public ActionResult GetFunction()
        {
            var json = new SYS_FunctionManager().GetFunction((Session["SYSUSER"] as SYS_User).UserId);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult GetUserSummary()
        {
            var json = JObject.Parse(new DataService(WXAPP.AppId, WXAPP.AppSecret).GetUserSummary(7));
            //var json = JObject.Parse("{\"list\":[{\"ref_date\":\"2019-03-07\",\"user_source\":0,\"new_user\":0,\"cancel_user\":0},{\"ref_date\":\"2019-03-07\",\"user_source\":17,\"new_user\":2,\"cancel_user\":0},{\"ref_date\":\"2019-03-07\",\"user_source\":30,\"new_user\":11,\"cancel_user\":3},{\"ref_date\":\"2019-03-09\",\"user_source\":0,\"new_user\":0,\"cancel_user\":2}]}");
            if(json["errcode"] != null)
            {
                return null;
            }
            var new_user = 0;
            var cancel_user = 0;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var i in json["list"])
            {
                string key = Convert.ToInt32(i["user_source"]).GetSummary();
                int value = Convert.ToInt32(i["new_user"]);
                new_user += Convert.ToInt32(i["new_user"]);
                cancel_user += Convert.ToInt32(i["cancel_user"]);
                if (dic.Keys.Contains(key))
                {
                    dic[key] += value;
                }
                else
                {
                    dic.Add(key, value);
                }
            }

            List<SummaryHelper> list = new List<SummaryHelper>();
            foreach (var i in dic.Keys)
            {
                list.Add(new SummaryHelper() { key = i, val = dic[i] });
            }
            var respJson = new
            {
                list = list,
                new_user = new_user,
                cancel_user = cancel_user,
            };
            return Json(respJson, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserCumulate()
        {
            var str = new DataService(WXAPP.AppId, WXAPP.AppSecret).GetUserCumulate(7);
            return Json(str, JsonRequestBehavior.AllowGet);
        }
    }
}