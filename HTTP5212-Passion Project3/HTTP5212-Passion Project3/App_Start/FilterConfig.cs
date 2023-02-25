using System.Web;
using System.Web.Mvc;

namespace HTTP5212_Passion_Project3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
