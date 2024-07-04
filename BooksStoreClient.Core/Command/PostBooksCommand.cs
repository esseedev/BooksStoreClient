using Common.Commands;
using Common.Dto;

namespace BooksStoreClient.Core.Command;

public class PostBooksCommand(BooksStoreClient booksStoreClient) : IPostBooksCommand
{
    private readonly BooksStoreClient _booksStoreClient = 
        booksStoreClient ?? throw new ArgumentNullException(nameof(booksStoreClient));

    public async Task Execute(BooksDto newBooks, CancellationToken cancellationToken)
    {
        await _booksStoreClient.PostBooks(newBooks, cancellationToken).ConfigureAwait(false);
    }
}