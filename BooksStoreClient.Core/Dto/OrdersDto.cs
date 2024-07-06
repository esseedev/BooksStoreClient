namespace BooksStoreClient.Core.Dto;

public sealed record OrdersDto(Guid OrderId, List<OrderLineDto> OrderLines);
