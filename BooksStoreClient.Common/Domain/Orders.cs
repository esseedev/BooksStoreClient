namespace Common.Domain;

public sealed record Orders(Guid OrderId, List<OrderLine> OrderLines);