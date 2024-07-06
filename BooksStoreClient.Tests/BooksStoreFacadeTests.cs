using System.Net;
using BooksStoreClient.Core;
using BooksStoreClient.Core.Commands;
using BooksStoreClient.Core.Queries;
using BooksStoreClient.Public;
using BooksStoreClient.Shared.Dto;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace BooksStoreClient.Tests;

public class BooksStoreFacadeTests
{
    private static readonly Guid OrderGuid = Guid.NewGuid();
    private const string BaseUrl = "http://localhost:5555";

    private IHttpClientFactory CreateMockFactory(MockHttpMessageHandler mockHttp)
    {
        var client = mockHttp.ToHttpClient();
        client.BaseAddress = new Uri(BaseUrl);
        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient("BooksStoreClient").Returns(client);
        return factory;
    }

    [Fact]
    public async Task GetBooksAsync_ShouldReturnBooks_FromQuery()
    {
        // Arrange
        var expectedBooks = new List<BookDto> { new BookDto(1, "Test Book", 10, 1, 1, new List<AuthorDto>()) };
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, $"{BaseUrl}{BooksStoreConstants.BooksPath}")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(expectedBooks));

        var mockFactory = CreateMockFactory(mockHttp);
        var query = new GetAllBooksQuery(mockFactory);

        var facade = new BooksStoreFacade(
            query,
            Substitute.For<GetAllOrdersQuery>(mockFactory),
            Substitute.For<PostBooksCommandHandler>(mockFactory)
        );

        // Act
        var result = await facade.GetBooksAsync(CancellationToken.None);

        // Assert
        Assert.Equal(expectedBooks.Count, result.Count);
        Assert.Equal(expectedBooks[0].Title, result.First().Title);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrdersAsync_ShouldReturnOrders_FromQuery()
    {
        // Arrange
        var expectedOrders = new List<OrderDto> { new OrderDto(OrderGuid, new List<OrderLineDto>()) };
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, $"{BaseUrl}{BooksStoreConstants.OrdersPath}")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(expectedOrders));

        var mockFactory = CreateMockFactory(mockHttp);
        var query = new GetAllOrdersQuery(mockFactory);

        var facade = new BooksStoreFacade(
            Substitute.For<GetAllBooksQuery>(mockFactory),
            query,
            Substitute.For<PostBooksCommandHandler>(mockFactory)
        );

        // Act
        var result = await facade.GetOrdersAsync(CancellationToken.None);

        // Assert
        Assert.Equal(expectedOrders.Count, result.Count);
        Assert.Equal(expectedOrders[0].OrderId, result.First().OrderId);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task PostBooksAsync_ShouldCallCommandHandler()
    {
        // Arrange
        var newBooks = new List<BookDto> { new BookDto(1, "New Book", 15, 2, 3, new List<AuthorDto>()) };
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, $"{BaseUrl}{BooksStoreConstants.BooksPath}")
            .WithContent(System.Text.Json.JsonSerializer.Serialize(newBooks))
            .Respond(HttpStatusCode.Created);

        var mockFactory = CreateMockFactory(mockHttp);
        var handler = new PostBooksCommandHandler(mockFactory);

        var facade = new BooksStoreFacade(
            Substitute.For<GetAllBooksQuery>(mockFactory),
            Substitute.For<GetAllOrdersQuery>(mockFactory),
            handler
        );

        // Act
        await facade.PostBooksAsync(newBooks, CancellationToken.None);

        // Assert
        mockHttp.VerifyNoOutstandingExpectation();
    }
}