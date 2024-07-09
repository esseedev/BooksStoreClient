using System.Text.Json;
using BooksStoreClient.Core.Queries;
using BooksStoreClient.Shared.Dto;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace BooksStoreClient.Tests;

public class GetAllOrdersQueryTests
{
    private readonly Guid OrderGuid = new Guid();
    [Fact]
    public async Task ExecuteAsync_ShouldReturnOrders_WhenApiReturnsData()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        var orders = new List<OrderDto> { new (OrderGuid, new List<OrderLineDto>()) };
        var jsonResponse = JsonSerializer.Serialize(orders);

        mockHttp.Expect(HttpMethod.Get, "http://localhost:5555/api/orders")
            .Respond("application/json", jsonResponse);

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient("BooksStoreClient").Returns(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost:5555") });

        var query = new GetAllOrdersQuery(factory);

        // Act
        var result = await query.ExecuteAsync(CancellationToken.None);

        // Assert
        Assert.Single(result);
        Assert.Equal(OrderGuid, result.First().OrderId);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenApiReturnsNoData()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, "http://localhost:5555/api/orders")
            .Respond("application/json", "[]");

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient("BooksStoreClient").Returns(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost:5555") });

        var query = new GetAllOrdersQuery(factory);

        // Act
        var result = await query.ExecuteAsync(CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }
}