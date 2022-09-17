using Library.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Infrastructure.Contexts
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> option) : base(option)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Book>().Property(y => y.GenreId).HasDefaultValue(1);

            foreach(var genre in GenresData)
            {
                modelBuilder.Entity<Genre>().HasData(genre);
            }
            
        }

        #region Data

        private IEnumerable<Book> GenresData
        {
            get
            {
                yield return new Book()
                {
                    Id = 1,
                    Name = "Детектив",
                };
                yield return new Book()
                {
                    Id = 2,
                    Name = "Фантастика",
                };
                yield return new Book()
                {
                    Id = 3,
                    Name = "Роман",
                };
            }
        }

        #endregion
    }
}
