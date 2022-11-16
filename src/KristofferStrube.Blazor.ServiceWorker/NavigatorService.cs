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
        return await this.MemoizedTask(async () =>
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSNavigator = await helper.InvokeAsync<IJSObjectReference>("getNavigator");
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", jSNavigator, "serviceWorker");
        return new ServiceWorkerContainer(jSRuntime, jSInstance);
    });
    }
}
