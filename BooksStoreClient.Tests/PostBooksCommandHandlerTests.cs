using System.Net;
using BooksStoreClient.Core.Commands;
using BooksStoreClient.Shared.Dto;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace BooksStoreClient.Tests;

public class PostBooksCommandHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendPostRequest_WithCorrectData()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:5555/api/books")
            .Respond(HttpStatusCode.Created);

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient("BooksStoreClient").Returns(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost:5555") });

        var handler = new PostBooksCommandHandler(factory);
        var command = new PostBooksCommand(new List<BookDto> { new BookDto(1, "Test Book", 10, 1, 1, new List<AuthorDto>()) }, CancellationToken.None);

        // Act
        await handler.HandleAsync(command);

        // Assert
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task HandleAsync_ShouldThrowException_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:5555/api/books")
            .Respond(HttpStatusCode.BadRequest);

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient("BooksStoreClient").Returns(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost:5555") });

        var handler = new PostBooksCommandHandler(factory);
        var command = new PostBooksCommand(new List<BookDto> { new BookDto(1, "Test Book", 10, 1, 1, new List<AuthorDto>()) }, CancellationToken.None);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => handler.HandleAsync(command));
    }
}