using Common.Dto;

namespace Common.Commands;

public interface IPostBooksCommand
{
    Task Execute(BooksDto newBooks, CancellationToken cancellationToken);
}