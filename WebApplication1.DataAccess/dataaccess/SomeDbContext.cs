using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataAccess;
using WebApplication1.Domain;

namespace WebApplication1.Data.dataaccess
{
    public class SomeDbContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }

        public SomeDbContext(DbContextOptions<SomeDbContext> options) : base(options)
        {
            var isMock = false;
            if (isMock)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
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
            modelBuilder.ApplyConfiguration(new AttachmentEntityTypeConfiguration());
        }
    }
}
