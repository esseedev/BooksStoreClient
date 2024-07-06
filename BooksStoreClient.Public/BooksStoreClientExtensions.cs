using BooksStoreClient.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BooksStoreClient.Public;

public static class BooksStoreClientExtensions
{
    public static IServiceCollection AddBooksStoreClient(this IServiceCollection services,
        Action<BooksStoreSettings> configureSettings)
    {
        services.Configure(configureSettings);
        services.AddHttpClient<IBooksStoreClient, Core.BooksStoreClient>(client =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var settings = serviceProvider.GetRequiredService<IOptions<BooksStoreSettings>>().Value;

            client.BaseAddress = new Uri(settings.ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {settings.Token}");
        });
        return services;
    }
}