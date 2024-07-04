using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Common;
using Common.Domain;
using Common.Dto;
using Microsoft.Extensions.Options;

namespace BooksStoreClient.Core;

public sealed class BooksStoreClient(HttpClient httpClient, IOptions<BooksStoreSettings> settings): IBooksStoreClient
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly BooksStoreSettings _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));

    public async Task<IReadOnlyCollection<BooksDto>> GetBooks(CancellationToken cancellationToken)
    {
        var request = CreateGetRequest(_settings.ApiBaseUrl);
        using var result = await _httpClient
            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var books = await JsonSerializer.DeserializeAsync<List<BooksDto>>(contentStream, 
            DefaultJsonSerializerOptions.Options, cancellationToken) 
               ??  new List<BooksDto>();
        return new ReadOnlyCollection<BooksDto>(books);
    }

    // to optimize this more I would use Data Paging
    public async Task<IReadOnlyCollection<OrdersDto>> GetOrders(CancellationToken cancellationToken)
    {
        var request = CreateGetRequest(_settings.OrdersPath);
        using var result = await _httpClient
            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var orders = await JsonSerializer.DeserializeAsync<List<OrdersDto>>(contentStream,
            DefaultJsonSerializerOptions.Options, cancellationToken) 
               ?? new List<OrdersDto>(); //There should be null object pattern
        return new ReadOnlyCollection<OrdersDto>(orders);
    }

    public async Task PostBooks(BooksDto newBook, CancellationToken cancellationToken)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(newBook), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, _settings.BooksPath)
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