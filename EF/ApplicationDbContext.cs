using IO_B.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace IO_B.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Dodaj DbSet dla każdej encji
        public DbSet<YourEntity> YourEntities { get; set; }
    }
}
