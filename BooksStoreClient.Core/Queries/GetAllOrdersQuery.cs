using System.Collections.ObjectModel;
using System.Text.Json;
using BooksStoreClient.Shared.Dto;

namespace BooksStoreClient.Core.Queries;

public class GetAllOrdersQuery(IHttpClientFactory factory)
{
    private readonly IHttpClientFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    public async Task<IReadOnlyCollection<OrderDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var client = _factory.CreateClient("BooksStoreClient");
        using var result = await client
            .GetAsync(BooksStoreConstants.OrdersPath, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var orders = await JsonSerializer.DeserializeAsync<List<OrderDto>>(contentStream,
                 DefaultJsonSerializerOptions.Options, cancellationToken) 
             ?? new List<OrderDto>(); //There should be null object pattern
        return new ReadOnlyCollection<OrderDto>(orders);
    } 
}