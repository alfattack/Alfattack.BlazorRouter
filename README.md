## Blazor Router Wrapper

Adds some additional injection points to default navigation/routing to allow changing routes dynamically.

Nuget Package can be found here [Here](https://www.nuget.org/packages/Alfattack.BlazorRouter/).

## Setup

1. Add `builder.Services.AddAlfattackBlazorRouter();`
2. Implement and register `SubRouterPageService`.

## `SubRouterPageService` Usage
 Given a `string route` return `SubRouterPageData`.
```
public class SubRouterPageData
{
    public bool IsSubRoute { get; set; }
    public string? SubRoutePrefix { get; set; }
    public string? PageRouteWithoutSubRoutePrefix { get; set; }
    public string? PageRouteWithSubRoutePrefix { get; set; }
}
```
If we have some well known routes that are localised e.g. `/mypage` we'll want the following result when calling `SubRouterPageService.GetPageData(string route)`

``` C#
var routeData = _subPageRouteService.GetPageData("mypage");
var routeData = _subPageRouteService.GetPageData("en/mypage");

// new SubRouterPageData
// {
//    IsSubRoute = true,
//    SubRoutePrefix = "en"
//    PageRouteWithoutSubRoutePrefix = "mypage",
//    PageRouteWithSubRoutePrefix = "en/mypage",
// };

```

i.e. both the prefixed path, and the non-prefixed path should return the same data in our implementation.