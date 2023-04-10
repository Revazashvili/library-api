using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(author => author.Id);
        
        builder.Property(author => author.FirstName)
            .HasMaxLength(250)
            .IsRequired()
            .IsUnicode();
        
        builder.Property(author => author.LastName)
            .HasMaxLength(250)
            .IsRequired()
            .IsUnicode();
    }
}