using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfattack.BlazorRouter
{
    public class SubRouterPageData
    {
        public bool IsSubRoute { get; set; }
        public string? SubRoutePrefix { get; set; }
        public string? PageRouteWithoutSubRoutePrefix { get; set; }
        public string? PageRouteWithSubRoutePrefix { get; set; }
    }

    public class SubRouterPageService
    {
        public virtual Task<SubRouterPageData> GetPageData(string route) => Task.FromResult(new SubRouterPageData { IsSubRoute = false, SubRoutePrefix = "", PageRouteWithSubRoutePrefix = route });
    }
}