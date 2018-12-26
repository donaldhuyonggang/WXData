using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataModel;
using WXDataModel.Extend;

namespace WXDataUI.Models
{
    public class Controller_EX:Controller
    {
        public Controller_EX()
        {
            try
            {
                SYS_User user = HttpContext.Session["SYSUSER"] as SYS_User;
                var list = user.SYS_Role.GetMainFunction();
                HttpContext.Session["USERRIGHT"] = list;
            }
            catch { }
        }
    }
}