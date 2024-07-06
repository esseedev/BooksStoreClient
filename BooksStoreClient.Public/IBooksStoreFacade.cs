using BooksStoreClient.Shared.Dto;

namespace BooksStoreClient.Public;

public interface IBooksStoreFacade
{
    Task<IReadOnlyCollection<BookDto>> GetBooksAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken);
    Task PostBooksAsync(List<BookDto> newBooks, CancellationToken cancellationToken);
}