using BooksStoreClient.Shared.Dto;

namespace BooksStoreClient.Core.Commands;

public class PostBooksCommand(List<BookDto> newBooks, CancellationToken cancellationToken)
{
    public List<BookDto> NewBooks { get; } = newBooks ?? throw new ArgumentNullException(nameof(newBooks));
    public CancellationToken CancellationToken { get; } = cancellationToken;
}