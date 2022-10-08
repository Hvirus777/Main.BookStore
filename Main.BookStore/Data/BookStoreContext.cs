using Main.BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Main.BookStore.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {

        public BookStoreContext(DbContextOptions<BookStoreContext> optionsBuilder) : base(optionsBuilder)
        { 
        }

        public DbSet<Books> books { get; set; }
      
        public DbSet<Language> language { get; set; }
        public DbSet<GalleryOfBooks> galleryOfBooks { get; set; }

    }
}
