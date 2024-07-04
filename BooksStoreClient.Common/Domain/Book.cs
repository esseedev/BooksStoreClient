namespace Common.Domain;

public sealed record Book(int Id, string Title, decimal Price, int Bookstand, int Shelf, List<Author> Authors);