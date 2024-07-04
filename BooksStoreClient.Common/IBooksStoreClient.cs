using Common.Domain;
using Common.Dto;

namespace Common;

public interface IBooksStoreClient
{
    Task<IReadOnlyCollection<BooksDto>> GetBooks(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrdersDto>> GetOrders(CancellationToken cancellationToken);
    Task PostBooks(BooksDto newBook, CancellationToken cancellationToken);
}