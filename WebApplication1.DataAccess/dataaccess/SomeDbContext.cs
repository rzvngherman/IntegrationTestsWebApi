using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataAccess;

namespace WebApplication1.Data.dataaccess
{
    public class SomeDbContext : DbContext
    {
        public virtual DbSet<domain.Employee> Employees { get; set; }

        public SomeDbContext(DbContextOptions<SomeDbContext> options) : base(options)
        {

        }

        public SomeDbContext(DbContextOptions<SomeDbContext> options, bool isMock = false)
            : base(options)
        {
            if (isMock)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
        }
    }
}
