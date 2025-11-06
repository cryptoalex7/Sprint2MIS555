using Microsoft.EntityFrameworkCore;
using SkynetERP.Models;

namespace SkynetERP.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Vendor> Vendors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Employee entity
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employees");
            
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Department)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        });

        // Configure Vendor entity
        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.ToTable("Vendors");
            
            entity.HasKey(v => v.Id);
            
            entity.Property(v => v.CompanyName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(v => v.Category)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(v => v.ContactPerson)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(v => v.Address)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(v => v.Phone)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(v => v.AnnualSpend)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        });

        // Seed initial data
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Department = "IT",
                Role = "Senior Developer",
                Address = "123 Main St, City, State",
                Phone = "(555)-123-4567",
                Salary = 85000
            },
            new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Johnson",
                Department = "HR",
                Role = "HR Manager",
                Address = "456 Oak Ave, City, State",
                Phone = "(555)-234-5678",
                Salary = 75000
            },
            new Employee
            {
                Id = 3,
                FirstName = "Mike",
                LastName = "Davis",
                Department = "Finance",
                Role = "Financial Analyst",
                Address = "789 Pine St, City, State",
                Phone = "(555)-345-6789",
                Salary = 65000
            }
        );
    }
}

