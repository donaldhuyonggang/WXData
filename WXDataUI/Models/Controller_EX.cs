using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL.Base;
using WXDataModel;
using WXDataModel.Extend;

namespace WXDataUI.Models
{
    public class Controller_EX
    {
        //public static void BindSession(Controller con)
        //{
        //    try
        //    {
        //        HttpSessionStateBase Session = con.Session;
        //        SYS_User user = Session["SYSUSER"] as SYS_User;
        //        var list = user.SYS_Role.GetMainFunction();
        //        Session["USERRIGHT"] = list;
        //    }
        //    catch { }
        //}
        public static void BindSession(HttpSessionStateBase Session)
        {
            try
            {
                SYS_User user = new SYS_UserManager().GetByPK((Session["SYSUSER"] as SYS_User).UserId);
                var list = user.SYS_Role.GetMainFunction();
                Session["USERRIGHT"] = list;
                Session["SYSUSER"] = user;
            }
            catch { }
        }
    }
}