using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.DAL.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b=>b.Name).HasMaxLength(20).IsRequired();
            builder.Property(b=>b.Author).HasMaxLength(40).IsRequired();
            builder.Property(b=>b.Pages).HasDefaultValue(100).IsRequired();
        }
    }
}
