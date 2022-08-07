using BooksAPI.DAL.Configurations;
using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
