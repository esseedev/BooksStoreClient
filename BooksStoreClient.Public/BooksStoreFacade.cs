using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using BooksStoreClient.Core;
using BooksStoreClient.Core.Commands;
using BooksStoreClient.Core.Queries;
using BooksStoreClient.Shared.Dto;

namespace BooksStoreClient.Public;

public class BooksStoreFacade(
    GetAllBooksQuery getAllBooksQuery,
    GetAllOrdersQuery getAllOrdersQuery,
    PostBooksCommandHandler postBooksCommandHandler
    ) : IBooksStoreFacade
{

    private readonly GetAllBooksQuery _getAllBooksQuery =
        getAllBooksQuery ?? throw new ArgumentNullException(nameof(getAllBooksQuery));
    private readonly GetAllOrdersQuery _getAllOrdersQuery =
        getAllOrdersQuery ?? throw new ArgumentNullException(nameof(getAllOrdersQuery));
    private readonly PostBooksCommandHandler _postBooksCommandHandler =
        postBooksCommandHandler ?? throw new ArgumentNullException(nameof(postBooksCommandHandler));

    public async Task<IReadOnlyCollection<BooksDto>> GetBooksAsync(CancellationToken cancellationToken)
    {
        return await _getAllBooksQuery.ExecuteAsync(cancellationToken);
    }

    // to optimize this more I could use Data Paging
    public async Task<IReadOnlyCollection<OrdersDto>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        return await _getAllOrdersQuery.ExecuteAsync(cancellationToken);
    }

    public async Task PostBooksAsync(BooksDto newBook, CancellationToken cancellationToken)
    {
        var command = new PostBooksCommand(newBook, cancellationToken);
        await _postBooksCommandHandler.HandleAsync(command);
    }
}