using System.Web;
using System.Web.Mvc;

namespace N01520224_PassionProject_Reeservation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
