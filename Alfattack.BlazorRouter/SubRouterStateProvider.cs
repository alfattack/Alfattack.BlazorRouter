using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Alfattack.BlazorRouter
{
    public class SubRouterStateProvider : IRoutingStateProvider
    {
        public RouteData? RouteData { get; set; }
    }
}