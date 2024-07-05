using Common.Commands;
using Common.Dto;

namespace BooksStoreClient.Core.Command;

public class PostBooksCommandHandler(BooksStoreClient booksStoreClient) : IPostBooksCommandHandler
{
    private readonly BooksStoreClient _booksStoreClient = 
        booksStoreClient ?? throw new ArgumentNullException(nameof(booksStoreClient));

    public async Task Execute(BooksDto newBooks, CancellationToken cancellationToken)
    {
        // TODO: Create command with implementation
        await _booksStoreClient.PostBooks(newBooks, cancellationToken).ConfigureAwait(false);
    }
}