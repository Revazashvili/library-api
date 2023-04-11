namespace Domain.Entities;

public class Author
{
    public Author(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Book>? Books { get; set; }

    public static Author Create(string firstName, string lastName, params Book[] books) =>
        new(firstName, lastName)
        {
            Books = books.ToList()
        };
}