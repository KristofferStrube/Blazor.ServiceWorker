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
        var caches = await scope.GetCachesAsync();
        var cache = await caches.OpenAsync("v1");
        await cache.AddAsync("empty.html");
    };
    scope.OnActivate = async () =>
    {
        logger.WriteLine("We activated!");
        await Task.CompletedTask;
    };
    scope.OnFetch = async (fetchEvent) =>
    {
        var caches = await scope.GetCachesAsync();
        var request = await fetchEvent.GetRequestAsync();
        var url = await request.GetURLAsync();
        logger.WriteLine($"We fetched: {url}");
        await fetchEvent.RespondWithAsync(async () =>
        {
            var response = await caches.MatchAsync(new(request));
            if (response is not null)
            {
                return response;
            }

            var fetch = await scope.FetchAsync(new(request));
            if (await fetch.GetStatusAsync() == 404)
            {
                return await caches.MatchAsync(new("empty.html"));
            }
            return fetch;
        });
        await Task.CompletedTask;
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
        await Task.CompletedTask;
    };
    logger.WriteLine("We Initialized!");
    await Task.CompletedTask;
});

await app.RunAsync();