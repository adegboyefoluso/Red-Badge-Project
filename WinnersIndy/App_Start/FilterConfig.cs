using System.Web;
using System.Web.Mvc;

namespace WinnersIndy
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());// this place a global restriction on the application 
        }
    }
}
