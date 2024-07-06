namespace BooksStoreClient.Shared.Dto;

public sealed record OrdersDto(Guid OrderId, List<OrderLineDto> OrderLines);
