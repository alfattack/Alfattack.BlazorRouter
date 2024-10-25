using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Alfattack.Routing
{
    public class TenantRoutingStateProvider : IRoutingStateProvider
    {
        public RouteData? RouteData { get; set; }
    }
}