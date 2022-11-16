using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerContainer : BaseJSWrapper
{
    internal ServiceWorkerContainer(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<ServiceWorker?> GetControllerAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        if (await helper.InvokeAsync<bool>("isAttributeNullOrUndefined", JSReference, "controller"))
        {
            return null;
        }

        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "controller");
        return new ServiceWorker(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorkerRegistration> GetReadyAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttributeAsync", JSReference, "ready");
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorkerRegistration> RegisterAsync(string scriptURL, RegistrationOptions? options = null)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("register", scriptURL, options);
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorkerRegistration> RegisterAsync(string scriptBootstrapperURL, string rootPath, Func<ServiceWorkerGlobalScope, Task> script)
    {
        Guid id = Guid.Empty;
        await ScriptManager.AddScriptAsync(id, jSRuntime, this, script);
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("registerMessageListener", JSReference);
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("register", $"{scriptBootstrapperURL}?id={id}&root={rootPath}", new RegistrationOptions() { Type = WorkerType.Module });
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorkerRegistration> GetRegistrationAsync(string clientURL = "")
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("getRegistration", clientURL);
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task StartMessagesAsync()
    {
        await JSReference.InvokeVoidAsync("startMessages");
    }
}