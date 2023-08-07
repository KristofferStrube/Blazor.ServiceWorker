using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ResponsePromiseProxy
{
    private readonly Func<Task<ResponseProxy>> r;

    public ResponsePromiseProxy(Func<Task<ResponseProxy>> r)
    {
        this.r = r;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public DotNetObjectReference<ResponsePromiseProxy> ObjRef { get; set; }

    [JSInvokable]
    public async Task<Guid> Invoke()
    {
        return (await r()).Id;
    }
}
