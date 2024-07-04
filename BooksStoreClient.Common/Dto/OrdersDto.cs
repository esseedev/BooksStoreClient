using Common.Domain;

namespace Common.Dto;

public sealed record OrdersDto(Guid OrderId, List<OrderLine> OrderLines);
