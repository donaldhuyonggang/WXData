using System.Web.Mvc;

namespace WXDataUI.Areas.WXMedia
{
    public class WXMediaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WXMedia";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WXMedia_default",
                "WXMedia/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}