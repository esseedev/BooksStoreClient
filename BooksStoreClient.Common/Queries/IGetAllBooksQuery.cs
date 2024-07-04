using Common.Domain;
using Common.Dto;
namespace Common.Queries;

public interface IGetAllBooksQuery
{
    Task<IReadOnlyCollection<Books>> Execute(CancellationToken cancellationToken);
}