using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KristofferStrube.Blazor.ServiceWorker;
using KristofferStrube.Blazor.ServiceWorker.WasmExample;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddNavigatorService();

// This is a very simple logger.
builder.Services.AddSingleton<Logger>();

var app = builder.Build();

var navigator = app.Services.GetRequiredService<INavigatorService>();
var logger = app.Services.GetRequiredService<Logger>();
var serviceWorker = await navigator.GetServiceWorkerAsync();
await serviceWorker.RegisterAsync("./service-worker.js", async (scope) => {
    scope.OnInstall = async () =>
    {
        logger.WriteLine("We installed!");
        await Task.Yield();
    };
    scope.OnActivate = async () =>
    {
        logger.WriteLine("We activated!");
    };
    scope.OnFetch = async (fetchEvent) =>
    {
        logger.WriteLine("We fetched!");
        await Task.Yield();
    };
    scope.OnPush = async (pushEvent) =>
    {
        logger.WriteLine("We pushed!");
        var messageData = await pushEvent.GetDataAsync();
        var array = await messageData.ArrayBufferAsync();
        logger.WriteLine($"arrayBuffer length: {array.Length}");
        var blob = await messageData.BlobAsync();
        logger.WriteLine($"blob size: {await blob.GetSizeAsync()}");
        var json = await messageData.JsonProxyAsync();
        logger.WriteLine($"json object as string: {await json.AsStringAsync()}");
        var text = await messageData.TextAsync();
        logger.WriteLine($"text: {text}");
        await Task.Yield();
    };
    logger.WriteLine("We Initialized!");
    await Task.Yield();
});

await app.RunAsync();