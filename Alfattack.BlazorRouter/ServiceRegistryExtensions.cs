using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alfattack.BlazorRouter
{
    public static class ServiceRegistryExtensions
    {
        public static void AddPrefixedRouting(this IServiceCollection services)
        {
            services.AddSingleton<IRoutingStateProvider, SubRouterStateProvider>();
            services.AddScoped<SubRouterPageService>();
        }
    }
}