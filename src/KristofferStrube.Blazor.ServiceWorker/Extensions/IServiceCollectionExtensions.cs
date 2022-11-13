using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.ServiceWorker;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddNavigatorService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<INavigatorService, NavigatorService>();
    }
}
