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
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<User> Users { get; set; }

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

        // Configure Delivery entity
        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.ToTable("Deliveries");
            
            entity.HasKey(d => d.Id);
            
            entity.HasOne(d => d.Vendor)
                .WithMany()
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(d => d.DeliveryNumber)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(d => d.DeliveryDate)
                .IsRequired();
            
            entity.Property(d => d.Description)
                .HasMaxLength(500);
            
            entity.Property(d => d.Status)
                .HasMaxLength(100);
            
            entity.Property(d => d.PhotoPath)
                .HasMaxLength(500);
            
            entity.Property(d => d.CreatedAt)
                .IsRequired();
            
            entity.Property(d => d.CreatedBy)
                .HasMaxLength(100);
        });

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            
            entity.HasKey(u => u.Id);
            
            entity.HasIndex(u => u.Username)
                .IsUnique();
            
            entity.HasIndex(u => u.Email)
                .IsUnique();
            
            entity.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(u => u.CreatedAt)
                .IsRequired();
        });

        // Seed initial users (with hashed passwords)
        // Pre-computed SHA256 hashes for default passwords (Base64 encoded)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@erp.com",
                Username = "admin",
                Password = "XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=", // password
                Role = "Admin",
                CreatedAt = new DateTime(2024, 1, 1)
            },
            new User
            {
                Id = 2,
                FirstName = "HR",
                LastName = "Manager",
                Email = "hr@erp.com",
                Username = "hr",
                Password = "Bwo7Xo1L1cRqzMuRycVGFMDNZJ54xMRxnjpkJwuuXd8=", // hr123
                Role = "HR",
                CreatedAt = new DateTime(2024, 1, 1)
            },
            new User
            {
                Id = 3,
                FirstName = "Vendor",
                LastName = "User",
                Email = "vendor@erp.com",
                Username = "vendor",
                Password = "APwebGAoJHk8mEDngeXiB0dQfibd8NYPq5llZ6AyfN8=", // vendor123
                Role = "Vendor",
                CreatedAt = new DateTime(2024, 1, 1)
            },
            new User
            {
                Id = 4,
                FirstName = "Regular",
                LastName = "User",
                Email = "user@erp.com",
                Username = "user",
                Password = "5gbjiw2MGbJM8O44CBgxYup81j/3kS27IrXoAyhrREY=", // user123
                Role = "User",
                CreatedAt = new DateTime(2024, 1, 1)
            }
        );

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

