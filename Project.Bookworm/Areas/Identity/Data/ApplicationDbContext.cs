using ProjectBookworm.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Bookworm.Models;

namespace ProjectBookworm.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookContent> BookContents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>()
            .HasOne(b => b.BookContent)
            .WithOne(bc => bc.Book)
            .HasForeignKey<BookContent>(bc => bc.BookId);

    }
}
