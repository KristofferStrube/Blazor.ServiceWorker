namespace KristofferStrube.Blazor.ServiceWorker
{
    public interface INavigatorService
    {
        Task<ServiceWorkerContainer> GetServiceWorkerAsync();
    }
}