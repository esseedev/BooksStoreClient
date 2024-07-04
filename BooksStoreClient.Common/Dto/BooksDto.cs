using Common.Domain;

namespace Common.Dto;

public sealed record BooksDto(int Id, List<Book> BooksList);
