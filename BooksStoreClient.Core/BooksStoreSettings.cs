namespace BooksStoreClient.Core;

public sealed class BooksStoreSettings
{
    public string ApiBaseUrl { get; init; }
    public string BooksPath { get; init; }
    public string OrdersPath { get; init; }
    public string AuthorizationKey { get; init; }
}