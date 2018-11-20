using System.Web.Mvc;

namespace WXDataUI.Areas.WXCustom
{
    public class WXCustomAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WXCustom";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WXCustom_default",
                "WXCustom/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}