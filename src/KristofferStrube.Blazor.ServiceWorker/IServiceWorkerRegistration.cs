namespace KristofferStrube.Blazor.ServiceWorker
{
    public interface IServiceWorkerRegistration
    {
        Task<IServiceWorker?> GetActiveAsync();
        Task<IServiceWorker?> GetInstallingAsync();
        Task<IServiceWorker?> GetWaitingAsync();
    }
}