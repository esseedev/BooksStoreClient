using System.Text;
using System.Text.Json;

namespace BooksStoreClient.Core.Commands;

public class PostBooksCommandHandler(IHttpClientFactory factory)
{
    private readonly IHttpClientFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    public async Task HandleAsync(PostBooksCommand command)
    {
        var client = _factory.CreateClient("BooksStoreClient");
        var jsonContent = new StringContent(JsonSerializer.Serialize(command.NewBooks), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, BooksStoreConstants.BooksPath)
        {
            Content = jsonContent
        };
        
        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, command.CancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }
}