using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Reflection;

namespace Alfattack.BlazorRouter
{
    public partial class SubRouter
    {
        private Router router = default!;

        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        [Inject] public IRoutingStateProvider RoutingStateProvider { get; set; } = default!;

        [Inject] public SubRouterPageService SubRouterPageService { get; set; } = default!;

        /// <summary>
        /// Gets or sets the assembly that should be searched for components matching the URI.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public Assembly AppAssembly { get; set; } = default!;

        /// <summary>
        /// Gets or sets a collection of additional assemblies that should be searched for components
        /// that can match URIs.
        /// </summary>
        [Parameter] public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

        /// <summary>
        /// Gets or sets the content to display when no match is found for the requested route.
        /// </summary>
        [Parameter]
        public RenderFragment? NotFound { get; set; }

        /// <summary>
        /// Gets or sets the content to display when a match is found for the requested route.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public RenderFragment<RouteData>? Found { get; set; }

        /// <summary>
        /// Get or sets the content to display when asynchronous navigation is in progress.
        /// </summary>
        [Parameter] public RenderFragment? Navigating { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.RegisterLocationChangingHandler(OnLocationChanging);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var route = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
                var pageRouteData = await SubRouterPageService.GetPageData(NavigationManager.ToBaseRelativePath(NavigationManager.Uri));
                if (pageRouteData.IsSubRoute && !route.Contains(pageRouteData.SubRoutePrefix!))
                {
                    NavigationManager.NavigateTo(pageRouteData.PageRouteWithSubRoutePrefix!);
                }
            }
        }

        private async ValueTask OnLocationChanging(LocationChangingContext context)
        {
            var route = context.TargetLocation.Contains(NavigationManager.BaseUri)
                ? NavigationManager.ToBaseRelativePath(context.TargetLocation)
                : context.TargetLocation.TrimStart('/');

            var pageRouteData = await SubRouterPageService.GetPageData(route);

            // If the target location is a sub route, and doesn't have the prefix added, we'll prevent navigation.
            if (pageRouteData.IsSubRoute && !route.Contains(pageRouteData.SubRoutePrefix!))
            {
                context.PreventNavigation();
                NavigationManager.NavigateTo(pageRouteData.PageRouteWithSubRoutePrefix!);
            }
        }

        private Task OnNavigate(NavigationContext context) => CheckAndUpdateRouteState(context.Path);

        private async Task CheckAndUpdateRouteState(string navigationPathAndQuery)
        {
            if (!(RoutingStateProvider is SubRouterStateProvider subRouterStateProvider))
                return;

            if (router == null)
                return;

            subRouterStateProvider.RouteData = null;
            var pageRouteData = await SubRouterPageService.GetPageData(navigationPathAndQuery);

            if (pageRouteData.IsSubRoute)
            {
                var (pageType, parameters) = FindComponent(pageRouteData.PageRouteWithoutSubRoutePrefix!);
                if (pageType != null)
                {
                    subRouterStateProvider.RouteData = new RouteData(pageType, parameters ?? new Dictionary<string, object?>());
                }
            }
        }

        private (Type?, IReadOnlyDictionary<string, object?>?) FindComponent(string path)
        {
            var routerType = typeof(Router);
            var routerAssembly = routerType.Assembly;
            var routesProperty = routerType.GetProperty("Routes", BindingFlags.NonPublic | BindingFlags.Instance);
            var routeTable = routesProperty?.GetValue(router);
            var routeContextType = routerAssembly.GetTypes().FirstOrDefault(t => t.Name == "RouteContext")!;
            var routeContext = Activator.CreateInstance(routeContextType, new[] { path?.Split("?")?.ElementAtOrDefault(0) });
            var routeTableRouteMethod = routeTable!.GetType().GetMethod("Route");
            routeTableRouteMethod!.Invoke(routeTable, new[] { routeContext });

            return (routeContextType.GetProperty("Handler")!.GetValue(routeContext) as Type, routeContextType.GetProperty("Parameters")!.GetValue(routeContext) as IReadOnlyDictionary<string, object?>);
        }
    }
}