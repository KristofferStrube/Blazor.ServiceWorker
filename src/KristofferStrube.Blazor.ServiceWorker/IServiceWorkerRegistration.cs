namespace KristofferStrube.Blazor.ServiceWorker
{
    public interface IServiceWorkerRegistration
    {
        Task<IServiceWorker?> GetActiveAsync();
        Task<IServiceWorker?> GetInstallingAsync();
        Task<INavigationPreloadManager> GetNavigationPreloadAsync();
        Task<string> GetScopeAsync();
        Task<ServiceWorkerUpdateViaCache> GetUpdateViaCacheAsync();
        Task<IServiceWorker?> GetWaitingAsync();
        Task InvokeOnUpdateFound();
        Task<bool> UnregisterAsync();
        Task UpdateAsync();
        Func<Task>? OnUpdateFound { get; set; }
    }
}