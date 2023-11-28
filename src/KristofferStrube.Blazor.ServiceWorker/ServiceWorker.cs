using KristofferStrube.Blazor.ServiceWorker.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorker : BaseJSWrapper
{
    public static async Task<ServiceWorker> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        ServiceWorker serviceWorker = new(jSRuntime, jSReference);
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(serviceWorker), jSReference, "statechange", "InvokeOnStateChange");
        return serviceWorker;
    }

    public ServiceWorker(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<string> GetScriptURLAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "scriptURL");
    }

    public async Task<ServiceWorkerState> GetStateAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ServiceWorkerState>("getAttribute", JSReference, "state");
    }

    public async Task PostMessageAsync(Json json, IJSWrapper[]? transfer = null)
    {
        if (transfer is null)
        {
            await JSReference.InvokeVoidAsync("postMessage", json.JSReference);
        }
        else
        {
            await JSReference.InvokeVoidAsync("postMessage", json.JSReference, transfer.Select(t => t.JSReference).ToArray());
        }
    }

    public async Task PostMessageAsync(Json json, IJSObjectReference[] transfer)
    {
        await JSReference.InvokeVoidAsync("postMessage", json.JSReference, transfer);
    }

    public Func<Task>? OnStateChange { get; set; }

    public async Task InvokeOnStateChange()
    {
        if (OnStateChange is null)
        {
            return;
        }

        await OnStateChange();
    }
}
