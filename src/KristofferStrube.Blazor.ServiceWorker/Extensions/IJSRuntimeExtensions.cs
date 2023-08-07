using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker.Extensions;

public static class IJSRuntimeExtensions
{
    public static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.ServiceWorker/KristofferStrube.Blazor.ServiceWorker.js");
    }

    public static async Task<IJSInProcessObjectReference> GetInProcessHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.ServiceWorker/KristofferStrube.Blazor.ServiceWorker.js");
    }
}
