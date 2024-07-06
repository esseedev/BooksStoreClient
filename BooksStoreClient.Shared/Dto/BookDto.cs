namespace BooksStoreClient.Shared.Dto;

// For the price I would create Money object to ensure that no errors occur in the future
// decimal for reading, int for saving. To simplify things here I will use int
public sealed record BookDto(int Id, string Title, int Price, int Bookstand, int Shelf, List<AuthorDto> Authors);
