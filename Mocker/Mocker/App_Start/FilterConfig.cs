using System.Web.Mvc;
using Mocker.Filter;

namespace Mocker
{
    public class FilterConfig
    {

        //Global API filter goes here
        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            //filters.Add(new NotFoundActionFilterAttribute());
        }
    }

    
}
