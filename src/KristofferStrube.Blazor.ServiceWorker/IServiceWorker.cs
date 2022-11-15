namespace KristofferStrube.Blazor.ServiceWorker
{
    public interface IServiceWorker
    {
        Task<string> GetScriptURLAsync();
    }
}