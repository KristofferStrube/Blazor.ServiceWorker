using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public static class ScriptManager
{
    private static readonly Dictionary<Guid, ExecutionContext> scripts = new();

    public static async Task AddScriptAsync(Guid id, IJSRuntime jSRuntime, ServiceWorkerContainer container, Func<ServiceWorkerGlobalScopeProxy, Task> script)
    {
        ServiceWorkerGlobalScopeProxy scope = new(jSRuntime, id, container);
        await script(scope);
        scripts.Add(id, new(jSRuntime, container, scope, script));
    }

    [JSInvokable]
    public static async Task InvokeOnInstallAsync(string stringId, string eventId)
    {
        if (Guid.TryParse(stringId, out Guid id) &&
            scripts.TryGetValue(id, out ExecutionContext? context) &&
            context.Scope.OnInstall is not null)
        {
            await context.Scope.OnInstall.Invoke(new InstallEvent(context.JSRuntime, Guid.Parse(eventId), context.Container));
        }
    }

    [JSInvokable]
    public static async Task InvokeOnActivateAsync(string stringId, string _)
    {
        if (Guid.TryParse(stringId, out Guid id)
            && scripts.TryGetValue(id, out ExecutionContext? context)
            && context.Scope.OnActivate is not null)
        {
            await context.Scope.OnActivate.Invoke();
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

    [JSInvokable]
    public static async Task InvokeOnMessageAsync(string stringId, string eventId)
    {
        if (Guid.TryParse(stringId, out Guid id) &&
            scripts.TryGetValue(id, out ExecutionContext? context) &&
            context.Scope.OnMessage is not null)
        {
            await context.Scope.OnMessage.Invoke(new ExtendableMessageEvent(context.JSRuntime, Guid.Parse(eventId), context.Container));
        }
    }
}

public class ExecutionContext
{
    public IJSRuntime JSRuntime;
    public ServiceWorkerContainer Container;
    public ServiceWorkerGlobalScopeProxy Scope;
    public Func<ServiceWorkerGlobalScopeProxy, Task> Script;

    public ExecutionContext(IJSRuntime jSRuntime, ServiceWorkerContainer container, ServiceWorkerGlobalScopeProxy scope, Func<ServiceWorkerGlobalScopeProxy, Task> script)
    {
        JSRuntime = jSRuntime;
        Container = container;
        Scope = scope;
        Script = script;
    }
}
