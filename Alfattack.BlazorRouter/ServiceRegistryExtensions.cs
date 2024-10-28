using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alfattack.BlazorRouter
{
    public static class ServiceRegistryExtensions
    {
        public static void AddAlfattackBlazorRouter(this IServiceCollection services)
        {
            services.AddSingleton<IRoutingStateProvider, SubRouterStateProvider>();
            services.AddScoped<SubRouterPageService>();
        }
    }
}