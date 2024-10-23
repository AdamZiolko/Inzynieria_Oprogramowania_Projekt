using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Bookworm.Models;
using Project_Bookworm.Models;

namespace AuthSystem.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
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
