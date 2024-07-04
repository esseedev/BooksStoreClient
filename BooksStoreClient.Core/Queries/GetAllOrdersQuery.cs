using Common.Domain;
using Common.Queries;

namespace BooksStoreClient.Core.Queries;

public sealed class GetAllOrdersQuery(BooksStoreClient booksStoreClient) : IGetAllOrdersQuery
{
    private readonly BooksStoreClient _booksStoreClient =
        booksStoreClient ?? throw new ArgumentNullException(nameof(booksStoreClient));

    public async Task<IReadOnlyCollection<Orders>> Execute(CancellationToken cancellationToken)
    {
        var response = await _booksStoreClient.GetOrders(cancellationToken).ConfigureAwait(false);
        return response.Select(x => new Orders(x.OrderId, x.OrderLines)).ToArray();
    }
}