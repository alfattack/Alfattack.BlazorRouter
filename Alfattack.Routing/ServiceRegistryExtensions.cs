using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alfattack.Routing
{
    public static class ServiceRegistryExtensions
    {
        public static void AddPrefixedRouting(this IServiceCollection services)
        {
            services.AddSingleton<IRoutingStateProvider, TenantRoutingStateProvider>();
        }
    }
}
