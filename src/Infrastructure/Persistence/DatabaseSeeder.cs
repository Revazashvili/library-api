using Domain.Entities;

namespace Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task TrySeedAsync(LibraryDbContext context)
    {
        try
        {
            var williamShakespeare = Author.Create("William", "Shakespeare",
                Book.Create("Hamlet","The Tragic Story of Hamlet, Prince of Denmark"), 
                Book.Create("Macbeth","TheThree witches tell the Scottish general Macbeth that he will be King of Scotland. Encouraged by his wife, " +
                                      "Macbeth kills the king, becomes the new king, and kills more people out of paranoia. " +
                                      "Civil war erupts to overthrow Macbeth, resulting in more death."),
                Book.Create("Romeo and Juliet","Romeo and Juliet is a tragedy written by William Shakespeare early " +
                                               "in his career about the romance between two Italian youths from feuding families. " +
                                               "It was among Shakespeare's most popular plays during his lifetime and, along with Hamlet, " +
                                               "is one of his most frequently performed plays. " +
                                               "Today, the title characters are regarded as archetypal young lovers."));

            var charlesDickens = Author.Create("Charles", "Dickens",
                Book.Create("Oliver Twist","Oliver Twist; or, The Parish Boy's Progress, is the second novel by English author Charles Dickens. " +
                                           "It was originally published as a serial from 1837 to 1839, and as a three-volume book in 1838."),
                Book.Create("Great Expectations","Great Expectations is the thirteenth novel by Charles Dickens and his penultimate completed novel." +
                                                 " It depicts the education of an orphan nicknamed Pip (the book is a bildungsroman; a coming-of-age story)."));

            await context.Authors.AddRangeAsync(williamShakespeare, charlesDickens);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}