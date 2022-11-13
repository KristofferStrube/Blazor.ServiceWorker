using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerContainer : BaseJSWrapper
{
    internal ServiceWorkerContainer(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<ServiceWorker?> GetControllerAsync()
    {
        var helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "controller");
        if (jSInstance is null) return null;
        return new ServiceWorker(jSRuntime, jSInstance);
    }

    public async Task<ServiceWorkerRegistration> GetReadyAsync()
    {
        var helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttributeAsync", JSReference, "ready");
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task<ServiceWorkerRegistration> RegisterAsync(string scriptURL, RegistrationOptions? options = null)
    {
        var jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("register", scriptURL, options);
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task<ServiceWorkerRegistration> RegisterAsync(string scriptBootstrapperURL, Func<ServiceWorkerGlobalScope, Task> script)
    {
        Guid id = Guid.Empty;
        await ScriptManager.AddScriptAsync(id, jSRuntime, this, script);
        var helper = await helperTask.Value;
        await helper.InvokeVoidAsync("registerMessageListener", JSReference);
        var jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("register", $"{scriptBootstrapperURL}?id={id}", new RegistrationOptions() { Type = WorkerType.Module });
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task<ServiceWorkerRegistration> GetRegistrationAsync(string clientURL = "")
    {
        var jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("getRegistration", clientURL);
        return new ServiceWorkerRegistration(jSRuntime, jSInstance);
    }

    public async Task StartMessagesAsync()
    {
        await JSReference.InvokeVoidAsync("startMessages");
    }
}