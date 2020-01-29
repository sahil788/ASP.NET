using System.Web;
using System.Web.Mvc;

namespace Web_App_Project_Template_v11
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
