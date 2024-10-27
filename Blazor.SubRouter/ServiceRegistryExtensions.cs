using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.SubRouter
{
    public static class ServiceRegistryExtensions
    {
        public static void AddPrefixedRouting(this IServiceCollection services)
        {
            services.AddSingleton<IRoutingStateProvider, SubRouterStateProvider>();
        }
    }
}
