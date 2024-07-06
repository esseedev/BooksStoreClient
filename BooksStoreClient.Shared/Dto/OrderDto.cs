namespace BooksStoreClient.Shared.Dto;

public sealed record OrderDto(Guid OrderId, List<OrderLineDto> OrderLines);
