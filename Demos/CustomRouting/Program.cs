using Alfattack.BlazorRouter;
using CustomRouting;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAlfattackBlazorRouter();
builder.Services.AddScoped<SubRouterPageService, LocalisedSubRoutePageService>();

await builder.Build().RunAsync();
