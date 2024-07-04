using Common.Domain;
using Common.Dto;

namespace Common.Queries;

public interface IGetAllOrdersQuery
{
    Task<IReadOnlyCollection<Orders>> Execute(CancellationToken cancellationToken);
}