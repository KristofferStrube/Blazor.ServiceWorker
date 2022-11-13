using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class NavigatorService : INavigatorService
{
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSRuntime jSRuntime;

    public NavigatorService(IJSRuntime jSRuntime)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        this.jSRuntime = jSRuntime;
    }

    public async Task<ServiceWorkerContainer> GetServiceWorkerAsync()
    {
        var helper = await helperTask.Value;
        var jSNavigator = await helper.InvokeAsync<IJSObjectReference>("getNavigator");
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", jSNavigator, "serviceWorker");
        return new ServiceWorkerContainer(jSRuntime, jSInstance);
    }
}
