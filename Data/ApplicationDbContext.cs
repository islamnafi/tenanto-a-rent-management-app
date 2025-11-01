using Microsoft.EntityFrameworkCore;
using tenanto.Models;

namespace tenanto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<RentRecord> RentRecords { get; set; }
    }
}
