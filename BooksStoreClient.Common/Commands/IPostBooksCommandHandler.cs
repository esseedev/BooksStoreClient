using Common.Dto;

namespace Common.Commands;

public interface IPostBooksCommandHandler
{
    Task Execute(BooksDto newBooks, CancellationToken cancellationToken);
}