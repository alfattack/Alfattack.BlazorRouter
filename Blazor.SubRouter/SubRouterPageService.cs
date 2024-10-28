using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SubRouter
{
    public class SubRouterPageData
    {
        public bool IsSubRoute { get; set; }
        public string? SubRoutePrefix { get; set; }
        public string? SubRoute { get; set; }
        public string? FullRoute { get; set; }
    }

    public class SubRouterPageService
    {
        public virtual Task<SubRouterPageData> GetPageData(string route) => Task.FromResult(new SubRouterPageData { IsSubRoute = false, SubRoutePrefix = "", FullRoute = route });
    }
}
