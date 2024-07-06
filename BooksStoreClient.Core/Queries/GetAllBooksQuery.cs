using System.Collections.ObjectModel;
using System.Text.Json;
using BooksStoreClient.Shared.Dto;

namespace BooksStoreClient.Core.Queries;

public class GetAllBooksQuery(IHttpClientFactory factory)
{
    private readonly IHttpClientFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    public async Task<IReadOnlyCollection<BooksDto>> Execute(CancellationToken cancellationToken)
    {
        var client = _factory.CreateClient("BooksStoreClient");
        using var result = await client
            .GetAsync(BooksStoreConstants.BooksPath, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var books = await JsonSerializer.DeserializeAsync<List<BooksDto>>(contentStream, 
                        DefaultJsonSerializerOptions.Options, cancellationToken) 
                    ??  new List<BooksDto>(); //There should be null object pattern
        return new ReadOnlyCollection<BooksDto>(books);
    }
}