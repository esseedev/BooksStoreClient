namespace BooksStoreClient.Core.Dto;

public sealed record OrderLineDto(int BookId, int Quantity);