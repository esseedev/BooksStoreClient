using System.Text.Json;

namespace BooksStoreClient.Core;

public static class DefaultJsonSerializerOptions
{
    public static JsonSerializerOptions Options => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

}