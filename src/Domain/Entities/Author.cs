namespace Domain.Entities;

public class Author
{
    public Author(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public IEnumerable<Book>? Books { get; init; }
}