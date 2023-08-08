using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KristofferStrube.Blazor.ServiceWorker;
using KristofferStrube.Blazor.ServiceWorker.WasmExample;
using Microsoft.JSInterop;
using KristofferStrube.Blazor.Streams;

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
var environment = app.Services.GetRequiredService<IWebAssemblyHostEnvironment>();

var rootPath = environment.IsProduction() ? "/Blazor.ServiceWorker" : "";

var serviceWorker = await navigator.GetServiceWorkerAsync();
var registration = await serviceWorker.RegisterAsync("./service-worker.js", rootPath, async (scope) =>
{
    scope.OnInstall = async () =>
    {
        logger.WriteLine("We installed!");
        var caches = await scope.GetCachesAsync();
        var cache = await caches.OpenAsync("v1");
        await cache.AddAsync("404.html");
        await cache.AddAsync("empty.html");
    };
    scope.OnActivate = async () =>
    {
        logger.WriteLine("We activated!");
        //var clients = await scope.GetClientsAsync();
        //await clients.ClaimAsync();
    };
    scope.OnFetch = async (fetchEvent) =>
    {
        CacheStorageProxy caches = await scope.GetCachesAsync();
        Request request = await fetchEvent.GetRequestAsync();
        string url = await request.GetURLAsync();
        logger.WriteLine($"We fetched: {url}");
        await fetchEvent.RespondWithAsync(async () =>
        {
            ResponseProxy? response = await caches.MatchAsync(request);
            if (response is not null)
            {
                return response;
            }

            if (url.Contains("download/"))
            {
                response = await scope.FetchAsync(request);
                ReadableStreamProxy body = await response.GetBodyAsync();
                return await scope.ConstructResponse(body, new ResponseInit()
                {
                    Headers = new()
                    {
                        { "Content-Disposition", $"attachment; filename=\"image_{Random.Shared.Next(9999):D4}.jpg\"" },
                        { "Content-Type", "image/png" }
                    }
                });
            }

            if (url.Contains("mountain.jpg"))
            {
                string replacement = Random.Shared.Next(2) is 1 ? "snow" : "lighthouse";
                return await scope.FetchAsync(url.Replace("mountain", replacement));
            }

            return await scope.FetchAsync(request);
        });
    };
    scope.OnPush = async (pushEvent) =>
    {
        logger.WriteLine("We pushed!");
        var messageData = await pushEvent.GetDataAsync();
        var array = await messageData.ArrayBufferAsync();
        var blob = await messageData.BlobAsync();
        var json = await messageData.JsonProxyAsync();
        var text = await messageData.TextAsync();
        logger.WriteLine($"arrayBuffer length: {array.Length}");
        logger.WriteLine($"blob size: {await blob.GetSizeAsync()}");
        logger.WriteLine($"json object as string: {await json.AsStringAsync()}");
        logger.WriteLine($"text: {text}");
    };
    logger.WriteLine("We Initialized!");
    await Task.CompletedTask;
});

await app.RunAsync();