namespace Domain.Entities;

public class Book
{
    public int Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Author Author { get; set; }

    public static Book Create(string title, string description, int? id = null) =>
        new()
        {
            Id = id ?? default,
            Title = title,
            Description = description
        };
}