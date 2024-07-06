using BooksStoreClient.Shared.Dto;

namespace BooksStoreClient.Public;

public interface IBooksStoreFacade
{
    Task<IReadOnlyCollection<BooksDto>> GetBooksAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrdersDto>> GetOrdersAsync(CancellationToken cancellationToken);
    Task PostBooksAsync(BooksDto newBook, CancellationToken cancellationToken);
}