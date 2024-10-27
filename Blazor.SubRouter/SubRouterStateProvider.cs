using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Blazor.SubRouter
{
    public class SubRouterStateProvider : IRoutingStateProvider
    {
        public RouteData? RouteData { get; set; }
    }
}