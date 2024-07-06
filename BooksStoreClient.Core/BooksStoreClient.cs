using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using BooksStoreClient.Core.Dto;

namespace BooksStoreClient.Core;

public class BooksStoreClient(HttpClient httpClient) : IBooksStoreClient
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<IReadOnlyCollection<BooksDto>> GetBooksAsync(CancellationToken cancellationToken)
    {
        using var result = await _httpClient
            .GetAsync(BooksStoreConstants.BooksPath, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var books = await JsonSerializer.DeserializeAsync<List<BooksDto>>(contentStream, 
            DefaultJsonSerializerOptions.Options, cancellationToken) 
               ??  new List<BooksDto>(); //There should be null object pattern
        return new ReadOnlyCollection<BooksDto>(books);
    }

    // to optimize this more I could use Data Paging
    public async Task<IReadOnlyCollection<OrdersDto>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        using var result = await _httpClient
            .GetAsync(BooksStoreConstants.OrdersPath, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var orders = await JsonSerializer.DeserializeAsync<List<OrdersDto>>(contentStream,
            DefaultJsonSerializerOptions.Options, cancellationToken) 
               ?? new List<OrdersDto>(); //There should be null object pattern
        return new ReadOnlyCollection<OrdersDto>(orders);
    }

    public async Task PostBooksAsync(BooksDto newBook, CancellationToken cancellationToken)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(newBook), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, BooksStoreConstants.OrdersPath)
        {
            Content = jsonContent
        };
        
        using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }

    private static HttpRequestMessage CreateGetRequest(string uri)
    {
        return new HttpRequestMessage(HttpMethod.Get, uri);
    }
}