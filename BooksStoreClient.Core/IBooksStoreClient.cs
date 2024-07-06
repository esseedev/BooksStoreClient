using BooksStoreClient.Core.Dto;

namespace BooksStoreClient.Core;

public interface IBooksStoreClient
{
    Task<IReadOnlyCollection<BooksDto>> GetBooksAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrdersDto>> GetOrdersAsync(CancellationToken cancellationToken);
    Task PostBooksAsync(BooksDto newBook, CancellationToken cancellationToken);
}