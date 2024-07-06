using BooksStoreClient.Core;
using BooksStoreClient.Core.Commands;
using BooksStoreClient.Core.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BooksStoreClient.Public;

public static class BooksStoreClientExtensions
{
    public static IServiceCollection AddBooksStoreClient(this IServiceCollection services,
        Action<BooksStoreSettings> configureSettings)
    {
        services.Configure(configureSettings);
        services.AddHttpClient("BooksStoreClient", (serviceProvider,client) =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<BooksStoreSettings>>().Value;
            Console.WriteLine($"Attempting to connect to: {settings.ApiBaseUrl}");
            client.BaseAddress = new Uri(settings.ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {settings.Token}");
        });

        services.AddScoped<IBooksStoreFacade, BooksStoreFacade>();
        services.AddScoped<GetAllBooksQuery>();
        services.AddScoped<GetAllOrdersQuery>();
        services.AddScoped<PostBooksCommandHandler>();
        
        return services;
    }
}