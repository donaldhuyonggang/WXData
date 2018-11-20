using System.Web.Mvc;

namespace WXDataUI.Areas.WXMenu
{
    public class WXMenuAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WXMenu";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WXMenu_default",
                "WXMenu/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}