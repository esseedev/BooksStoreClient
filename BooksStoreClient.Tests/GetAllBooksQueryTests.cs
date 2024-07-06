using System.Text.Json;
using BooksStoreClient.Core.Queries;
using BooksStoreClient.Shared.Dto;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace BooksStoreClient.Tests;

public class GetAllBooksQueryTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnBooks_WhenApiReturnsData()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        var books = new List<BookDto> { new BookDto(1, "Test Book", 10, 1, 1, new List<AuthorDto>()) };
        var jsonResponse = JsonSerializer.Serialize(books);

        mockHttp.Expect(HttpMethod.Get, "http://localhost:5555/api/books")
            .Respond("application/json", jsonResponse);

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient("BooksStoreClient").Returns(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost:5555") });

        var query = new GetAllBooksQuery(factory);

        // Act
        var result = await query.ExecuteAsync(CancellationToken.None);

        // Assert
        Assert.Single(result);
        Assert.Equal("Test Book", result.First().Title);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenApiReturnsNoData()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, "http://localhost:5555/api/books")
            .Respond("application/json", "[]");

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient("BooksStoreClient").Returns(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost:5555") });

        var query = new GetAllBooksQuery(factory);

        // Act
        var result = await query.ExecuteAsync(CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }
}