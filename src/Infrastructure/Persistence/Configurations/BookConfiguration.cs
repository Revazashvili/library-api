using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(author => author.Id);

        builder.Property(author => author.Title)
            .HasMaxLength(1000)
            .IsRequired()
            .IsUnicode();

        builder.Property(author => author.Description)
            .IsRequired()
            .IsUnicode();

        builder.HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}