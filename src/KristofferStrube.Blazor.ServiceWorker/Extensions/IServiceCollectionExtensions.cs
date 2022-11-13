using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddNavigatorService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<INavigatorService, NavigatorService>();
    }
}
