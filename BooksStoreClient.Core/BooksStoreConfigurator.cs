using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksStoreClient.Core;

public static class BooksStoreConfigurator
{
    public static void AddBooksStoreConfiguration(this IServiceCollection services)
    {
        // Daj dostep konsumentowi na wstrzykiwanie settingsow jak uri czy authorization
        services.AddHttpClient<BooksStoreClient>(client =>
        {
            client.BaseAddress = new Uri(BooksStoreConstants.ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Authorization", "bearer");
        });
    }
}