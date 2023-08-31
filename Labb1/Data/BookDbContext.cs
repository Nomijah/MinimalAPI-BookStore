using Labb1.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, IsAvailable = true, Author = "Stephen King", Title = "The Shining", Year = 1977, Genre = "Horror", Description = "A scary book." },
                new Book { Id = 2, IsAvailable = true, Author = "David Baldacci", Title = "The Winner", Year = 1997, Genre = "Thriller", Description = "It's about a winner." },
                new Book { Id = 3, IsAvailable = false, Author = "A.A. Milne", Title = "Winnie The Pooh", Year = 1926, Genre = "Children", Description = "It's about a bear." },
                new Book { Id = 4, IsAvailable = false, Author = "Douglas Adams", Title = "The Hitchhicker's Guide to the Galaxy", Year = 1979, Genre = "Science Fiction", Description = "A funny book." },
                new Book { Id = 5, IsAvailable = true, Author = "J.R.R. Tolkien", Title = "The Lord of the Rings", Year = 1954, Genre = "Fantasy", Description = "It's about a ring." }
                );
        }
    }
}
