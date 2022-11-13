using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

internal class ResponsePromise
{
    private readonly Func<Task<Response>> r;

    public ResponsePromise(Func<Task<Response>> r)
    {
        this.r = r;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public DotNetObjectReference<ResponsePromise> ObjRef { get; set; }

    [JSInvokable]
    public async Task<Guid> Invoke()
    {
        return (await r()).Id;
    }
}
