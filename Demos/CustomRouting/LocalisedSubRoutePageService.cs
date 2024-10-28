using Alfattack.BlazorRouter;

namespace CustomRouting
{
    public class LocalisedSubRoutePageService : SubRouterPageService
    {
        public override Task<SubRouterPageData> GetPageData(string route)
        {
            // Imagine the app is localised to English using some injected service.
            var localisationPrefix = "en";

            if (route.ToLower() == localisationPrefix || route.ToLower().StartsWith($"{localisationPrefix}/"))
            {
                return Task.FromResult(new SubRouterPageData
                {
                    IsSubRoute = true,
                    PageRouteWithSubRoutePrefix = route,
                    PageRouteWithoutSubRoutePrefix = route.Substring(localisationPrefix.Length, route.Length - localisationPrefix.Length),
                    SubRoutePrefix = localisationPrefix
                });
            }
            else
            {
                return Task.FromResult(new SubRouterPageData
                {
                    IsSubRoute = true,
                    PageRouteWithSubRoutePrefix = localisationPrefix + "/" + route,
                    PageRouteWithoutSubRoutePrefix = route,
                    SubRoutePrefix = localisationPrefix
                });
            }
        }
    }
}
