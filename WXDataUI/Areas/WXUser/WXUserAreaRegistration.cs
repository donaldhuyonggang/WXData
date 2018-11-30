using System.Web.Mvc;

namespace WXDataUI.Areas.WXUser
{
    public class WXUserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WXUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WXUser_default",
                "WXUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "WXDataUI.Areas.WXUser.Controllers" }
            );
        }
    }
}