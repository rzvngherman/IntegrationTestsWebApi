using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApplication1.Data.domain;

namespace WebApplication1.DataAccess
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name).HasColumnName("Name")
                .IsRequired();            
        }
    }
}
