using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerRegistration : BaseJSWrapper, IServiceWorkerRegistration
{
    internal static async Task<ServiceWorkerRegistration> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        ServiceWorkerRegistration serviceWorkerRegistration = new(jSRuntime, jSReference);
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(serviceWorkerRegistration), jSReference, "updatefound", "InvokeOnUpdateFound");
        return serviceWorkerRegistration;
    }

    internal ServiceWorkerRegistration(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<IServiceWorker?> GetInstallingAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        if (await helper.InvokeAsync<bool>("isAttributeNullOrUndefined", JSReference, "installing"))
        {
            return null;
        }

        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "installing");
        return new ServiceWorker(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorker?> GetWaitingAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        if (await helper.InvokeAsync<bool>("isAttributeNullOrUndefined", JSReference, "waiting"))
        {
            return null;
        }

        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "waiting");
        return new ServiceWorker(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorker?> GetActiveAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        if (await helper.InvokeAsync<bool>("isAttributeNullOrUndefined", JSReference, "active"))
        {
            return null;
        }

        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "active");
        return new ServiceWorker(jSRuntime, jSInstance);
    }

    public async Task<INavigationPreloadManager> GetNavigationPreloadAsync()
    {
        return await this.MemoizedTask(async () =>
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "navigationPreload");
        return new NavigationPreloadManager(jSRuntime, jSInstance);
    });
    }

    public async Task<string> GetScopeAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "scope");
    }

    public async Task<ServiceWorkerUpdateViaCache> GetUpdateViaCacheAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ServiceWorkerUpdateViaCache>("getAttribute", JSReference, "updateViaCache");
    }

    public async Task UpdateAsync()
    {
        await JSReference.InvokeVoidAsync("update");
    }

    public async Task<bool> UnregisterAsync()
    {
        return await JSReference.InvokeAsync<bool>("unregister");
    }

    public Func<Task>? OnUpdateFound { get; set; }

    [JSInvokable]
    public async Task InvokeOnUpdateFound()
    {
        if (OnUpdateFound is null)
        {
            return;
        }

        await OnUpdateFound();
    }
}