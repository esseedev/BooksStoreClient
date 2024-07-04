using System.Text.Json;

namespace BooksStoreClient.Core;

internal static class DefaultJsonSerializerOptions
{
    public static JsonSerializerOptions Options => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

}