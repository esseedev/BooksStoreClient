using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BooksStoreClient.Core;

public static class BooksStoreConfigurator
{
    public static void AddBooksStoreConfiguration(this IServiceCollection services)
    {
        services.AddHttpClient<BooksStoreClient>((serviceProvider, client) =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<BooksStoreSettings>>().Value;
            client.BaseAddress = new Uri(settings.ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Authorization", settings.AuthorizationKey);
        });
    }
}