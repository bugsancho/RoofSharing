using RoofSharing.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace RoofSharing.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new WelcomePageFilter());
        }
    }
}
