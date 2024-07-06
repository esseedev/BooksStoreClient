using BooksStoreClient.Shared.Dto;

namespace BooksStoreClient.Core.Commands;

public class PostBooksCommand(BooksDto newBooks, CancellationToken cancellationToken)
{
    public BooksDto NewBooks { get; } = newBooks ?? throw new ArgumentNullException(nameof(newBooks));
    public CancellationToken CancellationToken { get; } = cancellationToken;
}