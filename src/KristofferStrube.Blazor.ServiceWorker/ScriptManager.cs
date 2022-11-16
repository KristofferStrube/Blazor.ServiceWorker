using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public static class ScriptManager
{
    private static readonly Dictionary<Guid, ExecutionContext> scripts = new();

    public static async Task AddScriptAsync(Guid id, IJSRuntime jSRuntime, ServiceWorkerContainer container, Func<ServiceWorkerGlobalScope, Task> script)
    {
        ServiceWorkerGlobalScope scope = new ServiceWorkerGlobalScope(jSRuntime, id, container);
        await script(scope);
        scripts.Add(id, new(jSRuntime, container, scope, script));
    }

    [JSInvokable]
    public static async Task InvokeOnInstallAsync(string stringId, string eventId)
    {
        if (Guid.TryParse(stringId, out Guid id) && scripts.TryGetValue(id, out ExecutionContext? context))
        {
            if (context.Scope.OnInstall is not null)
            {
                await context.Scope.OnInstall.Invoke();
            }
        }
    }

    [JSInvokable]
    public static async Task InvokeOnActivateAsync(string stringId, string eventId)
    {
        if (Guid.TryParse(stringId, out Guid id) && scripts.TryGetValue(id, out ExecutionContext? context))
        {
            if (context.Scope.OnActivate is not null)
            {
                await context.Scope.OnActivate.Invoke();
            }
        }
    }

    [JSInvokable]
    public static async Task InvokeOnFetchAsync(string stringId, string eventId)
    {
        if (Guid.TryParse(stringId, out Guid id) &&
            scripts.TryGetValue(id, out ExecutionContext? context) &&
            context.Scope.OnFetch is not null)
        {
            await context.Scope.OnFetch.Invoke(new FetchEvent(context.JSRuntime, Guid.Parse(eventId), context.Container));
        }
    }

    [JSInvokable]
    public static async Task InvokeOnPushAsync(string stringId, string eventId)
    {
        if (Guid.TryParse(stringId, out Guid id) &&
            scripts.TryGetValue(id, out ExecutionContext? context) &&
            context.Scope.OnPush is not null)
        {
            await context.Scope.OnPush.Invoke(new PushEvent(context.JSRuntime, Guid.Parse(eventId), context.Container));
        }
    }
}

internal class ExecutionContext
{
    public IJSRuntime JSRuntime;
    public ServiceWorkerContainer Container;
    public ServiceWorkerGlobalScope Scope;
    public Func<ServiceWorkerGlobalScope, Task> Script;

    internal ExecutionContext(IJSRuntime jSRuntime, ServiceWorkerContainer container, ServiceWorkerGlobalScope scope, Func<ServiceWorkerGlobalScope, Task> script)
    {
        JSRuntime = jSRuntime;
        Container = container;
        Scope = scope;
        Script = script;
    }
}
