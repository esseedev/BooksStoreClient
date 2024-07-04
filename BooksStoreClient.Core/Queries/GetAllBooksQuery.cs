using Common.Domain;
using Common.Queries;

namespace BooksStoreClient.Core.Queries;

public sealed class GetAllBooksQuery(BooksStoreClient booksStoreClient) : IGetAllBooksQuery
{
    private readonly BooksStoreClient _booksStoreClient = 
        booksStoreClient ?? throw new ArgumentNullException(nameof(booksStoreClient));

    public async Task<IReadOnlyCollection<Books>> Execute(CancellationToken cancellationToken)
    {
        var response = await _booksStoreClient.GetBooks(cancellationToken).ConfigureAwait(false);
        return response.Select(x => new Books(x.Id, x.BooksList)).ToArray();
    }
}