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
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerReview> CustomerReviews { get; set; }
    
    // Financial Management entities (8 tables)
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<TaxRate> TaxRates { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<JournalEntry> JournalEntries { get; set; }
    public DbSet<JournalLine> JournalLines { get; set; }
    
    // Legacy entities (kept for backward compatibility)
    public DbSet<Category> Categories { get; set; }
    public DbSet<Revenue> Revenues { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Budget> Budgets { get; set; }

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

        // Configure Customer entity
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            
            entity.HasKey(c => c.Id);
            
            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.Company)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.Phone)
                .HasMaxLength(20);
            
            entity.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(c => c.Notes)
                .HasMaxLength(500);
            
            entity.Property(c => c.CreatedAt)
                .IsRequired();
        });

        // Configure CustomerReview entity
        modelBuilder.Entity<CustomerReview>(entity =>
        {
            entity.ToTable("CustomerReviews");
            
            entity.HasKey(r => r.Id);
            
            entity.HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(r => r.ReviewText)
                .IsRequired()
                .HasMaxLength(1000);
            
            entity.Property(r => r.Rating)
                .IsRequired();
            
            entity.Property(r => r.ReviewerName)
                .HasMaxLength(100);
            
            entity.Property(r => r.ReviewDate)
                .IsRequired();
        });

        // Seed CRM data
        SeedCRMData(modelBuilder);

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

        // Configure Financial Management entities
        ConfigureFinancialEntities(modelBuilder);
        
        // Seed Financial Management data
        SeedFinancialData(modelBuilder);
    }

    private void ConfigureFinancialEntities(ModelBuilder modelBuilder)
    {
        // Configure Account entity
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Accounts");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.AccountName).IsRequired().HasMaxLength(100);
            entity.Property(a => a.AccountType).IsRequired().HasMaxLength(50);
            entity.Property(a => a.BankName).HasMaxLength(100);
            entity.Property(a => a.AccountNumber).HasMaxLength(50);
            entity.Property(a => a.Balance).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(a => a.Description).HasMaxLength(200);
            entity.Property(a => a.CreatedAt).IsRequired();
        });

        // Configure Partner entity
        modelBuilder.Entity<Partner>(entity =>
        {
            entity.ToTable("Partners");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Type).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Email).HasMaxLength(200);
            entity.Property(p => p.Phone).HasMaxLength(20);
            entity.Property(p => p.Address).HasMaxLength(500);
            entity.Property(p => p.City).HasMaxLength(100);
            entity.Property(p => p.State).HasMaxLength(50);
            entity.Property(p => p.ZipCode).HasMaxLength(20);
            entity.Property(p => p.ContactPerson).HasMaxLength(100);
            entity.Property(p => p.Notes).HasMaxLength(500);
            entity.Property(p => p.CreatedAt).IsRequired();
        });

        // Configure TaxRate entity
        modelBuilder.Entity<TaxRate>(entity =>
        {
            entity.ToTable("TaxRates");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Rate).IsRequired().HasColumnType("decimal(5,2)");
            entity.Property(t => t.TaxType).HasMaxLength(50);
            entity.Property(t => t.Jurisdiction).HasMaxLength(100);
            entity.Property(t => t.Description).HasMaxLength(500);
            entity.Property(t => t.EffectiveDate).IsRequired();
            entity.Property(t => t.CreatedAt).IsRequired();
        });

        // Configure Invoice entity
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoices");
            entity.HasKey(i => i.Id);
            entity.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(100);
            entity.Property(i => i.CustomerName).IsRequired().HasMaxLength(200);
            entity.Property(i => i.InvoiceType).IsRequired().HasMaxLength(20);
            entity.Property(i => i.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(i => i.PaidAmount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(i => i.Balance).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(i => i.InvoiceDate).IsRequired();
            entity.Property(i => i.DueDate).IsRequired();
            entity.Property(i => i.Status).HasMaxLength(50);
            entity.Property(i => i.Description).HasMaxLength(500);
            entity.Property(i => i.CustomerEmail).HasMaxLength(200);
            entity.Property(i => i.CustomerPhone).HasMaxLength(20);
            entity.Property(i => i.Notes).HasMaxLength(500);
            entity.Property(i => i.CreatedAt).IsRequired();
            entity.HasOne(i => i.Partner).WithMany().HasForeignKey(i => i.PartnerId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configure InvoiceLine entity
        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.ToTable("InvoiceLines");
            entity.HasKey(il => il.Id);
            entity.Property(il => il.Description).IsRequired().HasMaxLength(200);
            entity.Property(il => il.Quantity).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(il => il.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(il => il.TaxRate).IsRequired().HasColumnType("decimal(5,2)");
            entity.Property(il => il.LineTotal).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(il => il.Notes).HasMaxLength(500);
            entity.Property(il => il.CreatedAt).IsRequired();
            entity.HasOne(il => il.Invoice).WithMany(i => i.InvoiceLines).HasForeignKey(il => il.InvoiceId).OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Payment entity
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.PaymentNumber).IsRequired().HasMaxLength(100);
            entity.Property(p => p.PayeeName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.PaymentType).IsRequired().HasMaxLength(20);
            entity.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(p => p.PaymentDate).IsRequired();
            entity.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Status).HasMaxLength(50);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.ReferenceNumber).HasMaxLength(100);
            entity.Property(p => p.Notes).HasMaxLength(500);
            entity.Property(p => p.CreatedBy).HasMaxLength(100);
            entity.Property(p => p.CreatedAt).IsRequired();
            entity.HasOne(p => p.Account).WithMany().HasForeignKey(p => p.AccountId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.Invoice).WithMany().HasForeignKey(p => p.InvoiceId).OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(p => p.Partner).WithMany().HasForeignKey(p => p.PartnerId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configure JournalEntry entity
        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.ToTable("JournalEntries");
            entity.HasKey(je => je.Id);
            entity.Property(je => je.EntryNumber).IsRequired().HasMaxLength(100);
            entity.Property(je => je.EntryDate).IsRequired();
            entity.Property(je => je.Description).IsRequired().HasMaxLength(200);
            entity.Property(je => je.Reference).HasMaxLength(50);
            entity.Property(je => je.Status).HasMaxLength(50);
            entity.Property(je => je.Notes).HasMaxLength(500);
            entity.Property(je => je.CreatedBy).HasMaxLength(100);
            entity.Property(je => je.CreatedAt).IsRequired();
        });

        // Configure JournalLine entity
        modelBuilder.Entity<JournalLine>(entity =>
        {
            entity.ToTable("JournalLines");
            entity.HasKey(jl => jl.Id);
            entity.Property(jl => jl.AccountCode).IsRequired().HasMaxLength(100);
            entity.Property(jl => jl.AccountName).HasMaxLength(200);
            entity.Property(jl => jl.Description).IsRequired().HasMaxLength(200);
            entity.Property(jl => jl.DebitAmount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(jl => jl.CreditAmount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(jl => jl.Notes).HasMaxLength(500);
            entity.Property(jl => jl.CreatedAt).IsRequired();
            entity.HasOne(jl => jl.JournalEntry).WithMany(je => je.JournalLines).HasForeignKey(jl => jl.JournalEntryId).OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void SeedFinancialData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2024, 1, 1);

        // Seed Accounts (8 records - at least 5)
        modelBuilder.Entity<Account>().HasData(
            new Account { Id = 1, AccountName = "Main Checking", AccountType = "Checking", BankName = "First National Bank", AccountNumber = "****1234", Balance = 125000.00m, Description = "Primary business checking account", IsActive = true, CreatedAt = seedDate },
            new Account { Id = 2, AccountName = "Savings Account", AccountType = "Savings", BankName = "First National Bank", AccountNumber = "****5678", Balance = 250000.00m, Description = "Business savings account", IsActive = true, CreatedAt = seedDate },
            new Account { Id = 3, AccountName = "Operating Account", AccountType = "Checking", BankName = "Commerce Bank", AccountNumber = "****9012", Balance = 75000.00m, Description = "Daily operations account", IsActive = true, CreatedAt = seedDate },
            new Account { Id = 4, AccountName = "Credit Card", AccountType = "Credit", BankName = "Business Credit", AccountNumber = "****3456", Balance = -5000.00m, Description = "Business credit card", IsActive = true, CreatedAt = seedDate },
            new Account { Id = 5, AccountName = "Petty Cash", AccountType = "Cash", BankName = "N/A", AccountNumber = "N/A", Balance = 5000.00m, Description = "Office petty cash fund", IsActive = true, CreatedAt = seedDate },
            new Account { Id = 6, AccountName = "Investment Account", AccountType = "Investment", BankName = "Investment Bank", AccountNumber = "****7890", Balance = 500000.00m, Description = "Long-term investment account", IsActive = true, CreatedAt = seedDate },
            new Account { Id = 7, AccountName = "Payroll Account", AccountType = "Checking", BankName = "First National Bank", AccountNumber = "****2468", Balance = 45000.00m, Description = "Payroll processing account", IsActive = true, CreatedAt = seedDate },
            new Account { Id = 8, AccountName = "Reserve Account", AccountType = "Savings", BankName = "Commerce Bank", AccountNumber = "****1357", Balance = 100000.00m, Description = "Emergency reserve fund", IsActive = true, CreatedAt = seedDate }
        );

        // Seed Partners (5+ records: 2 customers, 2 vendors, 1 both)
        modelBuilder.Entity<Partner>().HasData(
            new Partner { Id = 1, Name = "ABC Corporation", Type = "Customer", Email = "contact@abccorp.com", Phone = "(555) 111-2222", Address = "123 Business St", City = "New York", State = "NY", ZipCode = "10001", ContactPerson = "John Smith", Notes = "Primary customer", IsActive = true, CreatedAt = seedDate },
            new Partner { Id = 2, Name = "XYZ Industries", Type = "Customer", Email = "info@xyzind.com", Phone = "(555) 222-3333", Address = "456 Commerce Ave", City = "Los Angeles", State = "CA", ZipCode = "90001", ContactPerson = "Jane Doe", Notes = "Regular customer", IsActive = true, CreatedAt = seedDate },
            new Partner { Id = 3, Name = "Office Supply Co", Type = "Vendor", Email = "sales@officesupply.com", Phone = "(555) 333-4444", Address = "789 Vendor Blvd", City = "Chicago", State = "IL", ZipCode = "60601", ContactPerson = "Bob Johnson", Notes = "Office supplies vendor", IsActive = true, CreatedAt = seedDate },
            new Partner { Id = 4, Name = "Tech Solutions Inc", Type = "Vendor", Email = "contact@techsol.com", Phone = "(555) 444-5555", Address = "321 Tech Park", City = "Austin", State = "TX", ZipCode = "73301", ContactPerson = "Alice Williams", Notes = "IT services vendor", IsActive = true, CreatedAt = seedDate },
            new Partner { Id = 5, Name = "Global Enterprises", Type = "Both", Email = "billing@globalent.com", Phone = "(555) 555-6666", Address = "654 Partnership Way", City = "Seattle", State = "WA", ZipCode = "98101", ContactPerson = "Charlie Brown", Notes = "Customer and vendor", IsActive = true, CreatedAt = seedDate },
            new Partner { Id = 6, Name = "Mega Corp", Type = "Customer", Email = "ap@megacorp.com", Phone = "(555) 666-7777", Address = "987 Corporate Dr", City = "Boston", State = "MA", ZipCode = "02101", ContactPerson = "David Lee", Notes = "Large enterprise customer", IsActive = true, CreatedAt = seedDate }
        );

        // Seed TaxRates (5+ records)
        modelBuilder.Entity<TaxRate>().HasData(
            new TaxRate { Id = 1, Name = "State Sales Tax", Rate = 8.50m, TaxType = "Sales", Jurisdiction = "Kansas", Description = "Standard state sales tax", IsActive = true, EffectiveDate = seedDate, CreatedAt = seedDate },
            new TaxRate { Id = 2, Name = "Federal Income Tax", Rate = 21.00m, TaxType = "Income", Jurisdiction = "Federal", Description = "Corporate income tax rate", IsActive = true, EffectiveDate = seedDate, CreatedAt = seedDate },
            new TaxRate { Id = 3, Name = "Local Sales Tax", Rate = 2.00m, TaxType = "Sales", Jurisdiction = "Local", Description = "Local municipality tax", IsActive = true, EffectiveDate = seedDate, CreatedAt = seedDate },
            new TaxRate { Id = 4, Name = "VAT", Rate = 20.00m, TaxType = "VAT", Jurisdiction = "International", Description = "Value added tax for international", IsActive = true, EffectiveDate = seedDate, CreatedAt = seedDate },
            new TaxRate { Id = 5, Name = "Service Tax", Rate = 10.00m, TaxType = "Service", Jurisdiction = "State", Description = "Service tax rate", IsActive = true, EffectiveDate = seedDate, CreatedAt = seedDate },
            new TaxRate { Id = 6, Name = "Property Tax", Rate = 1.50m, TaxType = "Property", Jurisdiction = "Local", Description = "Business property tax", IsActive = true, EffectiveDate = seedDate, CreatedAt = seedDate }
        );

        // Seed Invoices (6 records - at least 5, mix of AR and AP)
        modelBuilder.Entity<Invoice>().HasData(
            new Invoice { Id = 1, InvoiceNumber = "INV-2024-001", CustomerName = "ABC Corporation", PartnerId = 1, InvoiceType = "AR", Amount = 50000.00m, PaidAmount = 50000.00m, Balance = 0.00m, InvoiceDate = new DateTime(2024, 1, 5), DueDate = new DateTime(2024, 2, 5), Status = "Paid", Description = "Q1 Service Package", CustomerEmail = "billing@abccorp.com", CustomerPhone = "(555) 111-2222", Notes = "Payment received", CreatedAt = seedDate },
            new Invoice { Id = 2, InvoiceNumber = "INV-2024-002", CustomerName = "XYZ Industries", PartnerId = 2, InvoiceType = "AR", Amount = 75000.00m, PaidAmount = 75000.00m, Balance = 0.00m, InvoiceDate = new DateTime(2024, 2, 10), DueDate = new DateTime(2024, 3, 10), Status = "Paid", Description = "Software Licensing", CustomerEmail = "finance@xyzind.com", CustomerPhone = "(555) 222-3333", Notes = "Paid on time", CreatedAt = seedDate },
            new Invoice { Id = 3, InvoiceNumber = "INV-2024-003", CustomerName = "Tech Solutions Inc", PartnerId = 4, InvoiceType = "AP", Amount = 35000.00m, PaidAmount = 0.00m, Balance = 35000.00m, InvoiceDate = new DateTime(2024, 3, 15), DueDate = new DateTime(2024, 4, 15), Status = "Pending", Description = "IT Services Invoice", CustomerEmail = "accounts@techsol.com", CustomerPhone = "(555) 444-5555", Notes = "Awaiting payment", CreatedAt = seedDate },
            new Invoice { Id = 4, InvoiceNumber = "INV-2024-004", CustomerName = "Global Enterprises", PartnerId = 5, InvoiceType = "AR", Amount = 120000.00m, PaidAmount = 0.00m, Balance = 120000.00m, InvoiceDate = new DateTime(2024, 3, 20), DueDate = new DateTime(2024, 4, 20), Status = "Pending", Description = "Annual Service Contract", CustomerEmail = "billing@globalent.com", CustomerPhone = "(555) 555-6666", Notes = "Large contract", CreatedAt = seedDate },
            new Invoice { Id = 5, InvoiceNumber = "INV-2024-005", CustomerName = "Office Supply Co", PartnerId = 3, InvoiceType = "AP", Amount = 15000.00m, PaidAmount = 15000.00m, Balance = 0.00m, InvoiceDate = new DateTime(2024, 4, 1), DueDate = new DateTime(2024, 5, 1), Status = "Paid", Description = "Office Supplies Invoice", CustomerEmail = "pay@officesupply.com", CustomerPhone = "(555) 333-4444", Notes = "Payment completed", CreatedAt = seedDate },
            new Invoice { Id = 6, InvoiceNumber = "INV-2024-006", CustomerName = "Mega Corp", PartnerId = 6, InvoiceType = "AR", Amount = 95000.00m, PaidAmount = 0.00m, Balance = 95000.00m, InvoiceDate = new DateTime(2024, 4, 10), DueDate = new DateTime(2024, 5, 10), Status = "Pending", Description = "Product Purchase", CustomerEmail = "ap@megacorp.com", CustomerPhone = "(555) 666-7777", Notes = "Awaiting approval", CreatedAt = seedDate }
        );

        // Seed InvoiceLines (10+ records - at least 5)
        modelBuilder.Entity<InvoiceLine>().HasData(
            new InvoiceLine { Id = 1, InvoiceId = 1, Description = "Consulting Services - Q1", Quantity = 100, UnitPrice = 500.00m, TaxRate = 8.50m, LineTotal = 54250.00m, Notes = "Hourly consulting", CreatedAt = seedDate },
            new InvoiceLine { Id = 2, InvoiceId = 2, Description = "Software License - Annual", Quantity = 1, UnitPrice = 75000.00m, TaxRate = 0, LineTotal = 75000.00m, Notes = "Annual license", CreatedAt = seedDate },
            new InvoiceLine { Id = 3, InvoiceId = 2, Description = "Support Package", Quantity = 1, UnitPrice = 10000.00m, TaxRate = 0, LineTotal = 10000.00m, Notes = "Premium support", CreatedAt = seedDate },
            new InvoiceLine { Id = 4, InvoiceId = 3, Description = "IT Infrastructure Setup", Quantity = 1, UnitPrice = 25000.00m, TaxRate = 8.50m, LineTotal = 27125.00m, Notes = "One-time setup", CreatedAt = seedDate },
            new InvoiceLine { Id = 5, InvoiceId = 3, Description = "Monthly Maintenance", Quantity = 3, UnitPrice = 2500.00m, TaxRate = 8.50m, LineTotal = 8137.50m, Notes = "3 months maintenance", CreatedAt = seedDate },
            new InvoiceLine { Id = 6, InvoiceId = 4, Description = "Enterprise Service Package", Quantity = 1, UnitPrice = 120000.00m, TaxRate = 0, LineTotal = 120000.00m, Notes = "Annual contract", CreatedAt = seedDate },
            new InvoiceLine { Id = 7, InvoiceId = 5, Description = "Office Supplies - Bulk Order", Quantity = 50, UnitPrice = 300.00m, TaxRate = 8.50m, LineTotal = 16275.00m, Notes = "Bulk purchase", CreatedAt = seedDate },
            new InvoiceLine { Id = 8, InvoiceId = 6, Description = "Product License - Enterprise", Quantity = 5, UnitPrice = 15000.00m, TaxRate = 0, LineTotal = 75000.00m, Notes = "5 licenses", CreatedAt = seedDate },
            new InvoiceLine { Id = 9, InvoiceId = 6, Description = "Training Services", Quantity = 20, UnitPrice = 1000.00m, TaxRate = 8.50m, LineTotal = 21700.00m, Notes = "Training hours", CreatedAt = seedDate },
            new InvoiceLine { Id = 10, InvoiceId = 1, Description = "Setup Fee", Quantity = 1, UnitPrice = 5000.00m, TaxRate = 8.50m, LineTotal = 5425.00m, Notes = "Initial setup", CreatedAt = seedDate }
        );

        // Seed Payments (6 records - at least 5, mix of Inflow and Outflow)
        modelBuilder.Entity<Payment>().HasData(
            new Payment { Id = 1, PaymentNumber = "PAY-2024-001", PayeeName = "Office Supply Co", PartnerId = 3, PaymentType = "Outflow", Amount = 2500.00m, PaymentDate = new DateTime(2024, 1, 8), AccountId = 1, InvoiceId = 5, PaymentMethod = "Check", Status = "Completed", Description = "Office supplies payment", ReferenceNumber = "CHK-001", Notes = "Monthly supplies", CreatedBy = "admin", CreatedAt = seedDate },
            new Payment { Id = 2, PaymentNumber = "PAY-2024-002", PayeeName = "Utility Company", PaymentType = "Outflow", Amount = 3200.00m, PaymentDate = new DateTime(2024, 2, 5), AccountId = 1, InvoiceId = null, PaymentMethod = "Wire Transfer", Status = "Completed", Description = "Utility bill payment", ReferenceNumber = "WT-002", Notes = "Electricity and water", CreatedBy = "admin", CreatedAt = seedDate },
            new Payment { Id = 3, PaymentNumber = "PAY-2024-003", PayeeName = "ABC Corporation", PartnerId = 1, PaymentType = "Inflow", Amount = 50000.00m, PaymentDate = new DateTime(2024, 2, 10), AccountId = 1, InvoiceId = 1, PaymentMethod = "Wire Transfer", Status = "Completed", Description = "Customer payment", ReferenceNumber = "WT-003", Notes = "Invoice payment", CreatedBy = "admin", CreatedAt = seedDate },
            new Payment { Id = 4, PaymentNumber = "PAY-2024-004", PayeeName = "XYZ Industries", PartnerId = 2, PaymentType = "Inflow", Amount = 75000.00m, PaymentDate = new DateTime(2024, 3, 5), AccountId = 1, InvoiceId = 2, PaymentMethod = "ACH", Status = "Completed", Description = "Customer payment", ReferenceNumber = "ACH-004", Notes = "Invoice payment", CreatedBy = "admin", CreatedAt = seedDate },
            new Payment { Id = 5, PaymentNumber = "PAY-2024-005", PayeeName = "Tech Solutions Inc", PartnerId = 4, PaymentType = "Outflow", Amount = 15000.00m, PaymentDate = new DateTime(2024, 3, 15), AccountId = 1, InvoiceId = 3, PaymentMethod = "Credit Card", Status = "Completed", Description = "Vendor payment", ReferenceNumber = "CC-005", Notes = "Partial payment", CreatedBy = "admin", CreatedAt = seedDate },
            new Payment { Id = 6, PaymentNumber = "PAY-2024-006", PayeeName = "Marketing Agency", PaymentType = "Outflow", Amount = 12000.00m, PaymentDate = new DateTime(2024, 4, 1), AccountId = 1, InvoiceId = null, PaymentMethod = "Check", Status = "Completed", Description = "Marketing services", ReferenceNumber = "CHK-006", Notes = "Q1 campaign", CreatedBy = "admin", CreatedAt = seedDate }
        );

        // Seed JournalEntries (5+ records)
        modelBuilder.Entity<JournalEntry>().HasData(
            new JournalEntry { Id = 1, EntryNumber = "JE-2024-001", EntryDate = new DateTime(2024, 1, 15), Description = "Monthly Revenue Recognition", Reference = "REV-001", Status = "Posted", Notes = "Q1 revenue entry", CreatedBy = "admin", CreatedAt = seedDate, PostedAt = new DateTime(2024, 1, 15) },
            new JournalEntry { Id = 2, EntryNumber = "JE-2024-002", EntryDate = new DateTime(2024, 1, 31), Description = "Salary Accrual", Reference = "SAL-001", Status = "Posted", Notes = "January payroll", CreatedBy = "admin", CreatedAt = seedDate, PostedAt = new DateTime(2024, 1, 31) },
            new JournalEntry { Id = 3, EntryNumber = "JE-2024-003", EntryDate = new DateTime(2024, 2, 28), Description = "Depreciation Entry", Reference = "DEP-001", Status = "Posted", Notes = "Monthly depreciation", CreatedBy = "admin", CreatedAt = seedDate, PostedAt = new DateTime(2024, 2, 28) },
            new JournalEntry { Id = 4, EntryNumber = "JE-2024-004", EntryDate = new DateTime(2024, 3, 31), Description = "Accrued Expenses", Reference = "ACC-001", Status = "Draft", Notes = "Q1 accruals", CreatedBy = "admin", CreatedAt = seedDate },
            new JournalEntry { Id = 5, EntryNumber = "JE-2024-005", EntryDate = new DateTime(2024, 4, 15), Description = "Revenue Adjustment", Reference = "ADJ-001", Status = "Posted", Notes = "Revenue correction", CreatedBy = "admin", CreatedAt = seedDate, PostedAt = new DateTime(2024, 4, 15) },
            new JournalEntry { Id = 6, EntryNumber = "JE-2024-006", EntryDate = new DateTime(2024, 4, 30), Description = "Month End Closing", Reference = "CLOSE-001", Status = "Draft", Notes = "April closing entries", CreatedBy = "admin", CreatedAt = seedDate }
        );

        // Seed JournalLines (10+ records - at least 5)
        modelBuilder.Entity<JournalLine>().HasData(
            new JournalLine { Id = 1, JournalEntryId = 1, AccountCode = "4000", AccountName = "Revenue", Description = "Product Sales", DebitAmount = 0, CreditAmount = 150000.00m, Notes = "Revenue recognition", CreatedAt = seedDate },
            new JournalLine { Id = 2, JournalEntryId = 1, AccountCode = "1200", AccountName = "Accounts Receivable", Description = "AR - Customer", DebitAmount = 150000.00m, CreditAmount = 0, Notes = "AR entry", CreatedAt = seedDate },
            new JournalLine { Id = 3, JournalEntryId = 2, AccountCode = "5000", AccountName = "Salaries Expense", Description = "January Salaries", DebitAmount = 45000.00m, CreditAmount = 0, Notes = "Salary expense", CreatedAt = seedDate },
            new JournalLine { Id = 4, JournalEntryId = 2, AccountCode = "2100", AccountName = "Accrued Salaries", Description = "Salary Payable", DebitAmount = 0, CreditAmount = 45000.00m, Notes = "Accrual", CreatedAt = seedDate },
            new JournalLine { Id = 5, JournalEntryId = 3, AccountCode = "6000", AccountName = "Depreciation Expense", Description = "Equipment Depreciation", DebitAmount = 5000.00m, CreditAmount = 0, Notes = "Monthly depreciation", CreatedAt = seedDate },
            new JournalLine { Id = 6, JournalEntryId = 3, AccountCode = "1500", AccountName = "Accumulated Depreciation", Description = "Accum Depreciation", DebitAmount = 0, CreditAmount = 5000.00m, Notes = "Accumulated depreciation", CreatedAt = seedDate },
            new JournalLine { Id = 7, JournalEntryId = 4, AccountCode = "5100", AccountName = "Utilities Expense", Description = "Accrued Utilities", DebitAmount = 3200.00m, CreditAmount = 0, Notes = "Utility accrual", CreatedAt = seedDate },
            new JournalLine { Id = 8, JournalEntryId = 4, AccountCode = "2200", AccountName = "Accrued Expenses", Description = "Accrued Utilities Payable", DebitAmount = 0, CreditAmount = 3200.00m, Notes = "Accrued liability", CreatedAt = seedDate },
            new JournalLine { Id = 9, JournalEntryId = 5, AccountCode = "1200", AccountName = "Accounts Receivable", Description = "AR Adjustment", DebitAmount = 5000.00m, CreditAmount = 0, Notes = "Revenue adjustment", CreatedAt = seedDate },
            new JournalLine { Id = 10, JournalEntryId = 5, AccountCode = "4000", AccountName = "Revenue", Description = "Revenue Correction", DebitAmount = 0, CreditAmount = 5000.00m, Notes = "Revenue correction", CreatedAt = seedDate }
        );
    }

    private void SeedCRMData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2024, 1, 1);
        var recentDate = DateTime.Now.AddDays(-7);

        // Seed Customers (at least 5 records)
        modelBuilder.Entity<Customer>().HasData(
            new Customer 
            { 
                Id = 1, 
                Name = "John Smith", 
                Company = "TechCorp Solutions", 
                Email = "john.smith@techcorp.com", 
                Phone = "(555) 111-2222", 
                Status = "Active", 
                LastContact = recentDate, 
                Notes = "Primary contact for enterprise solutions. Very responsive.", 
                CreatedAt = seedDate 
            },
            new Customer 
            { 
                Id = 2, 
                Name = "Sarah Johnson", 
                Company = "Global Industries Inc", 
                Email = "sarah.j@globalind.com", 
                Phone = "(555) 222-3333", 
                Status = "Active", 
                LastContact = DateTime.Now.AddDays(-3), 
                Notes = "Regular customer, quarterly orders. Excellent payment history.", 
                CreatedAt = seedDate 
            },
            new Customer 
            { 
                Id = 3, 
                Name = "Michael Chen", 
                Company = "Innovation Labs", 
                Email = "mchen@innovationlabs.com", 
                Phone = "(555) 333-4444", 
                Status = "Lead", 
                LastContact = DateTime.Now.AddDays(-14), 
                Notes = "New prospect. Interested in our premium services. Follow up needed.", 
                CreatedAt = seedDate 
            },
            new Customer 
            { 
                Id = 4, 
                Name = "Emily Rodriguez", 
                Company = "Startup Ventures", 
                Email = "emily@startupventures.com", 
                Phone = "(555) 444-5555", 
                Status = "Prospect", 
                LastContact = DateTime.Now.AddDays(-5), 
                Notes = "Early stage company. Potential for growth partnership.", 
                CreatedAt = seedDate 
            },
            new Customer 
            { 
                Id = 5, 
                Name = "David Williams", 
                Company = "Enterprise Systems", 
                Email = "d.williams@enterprisesys.com", 
                Phone = "(555) 555-6666", 
                Status = "Active", 
                LastContact = DateTime.Now.AddDays(-1), 
                Notes = "Long-term customer. Annual contract renewal coming up.", 
                CreatedAt = seedDate 
            },
            new Customer 
            { 
                Id = 6, 
                Name = "Lisa Anderson", 
                Company = "Digital Solutions Group", 
                Email = "lisa.anderson@digitalsolutions.com", 
                Phone = "(555) 666-7777", 
                Status = "Inactive", 
                LastContact = DateTime.Now.AddMonths(-3), 
                Notes = "Previous customer. No recent activity. May need re-engagement campaign.", 
                CreatedAt = seedDate 
            },
            new Customer 
            { 
                Id = 7, 
                Name = "Robert Taylor", 
                Company = "Mega Corp International", 
                Email = "rtaylor@megacorp.com", 
                Phone = "(555) 777-8888", 
                Status = "Active", 
                LastContact = DateTime.Now.AddDays(-2), 
                Notes = "Large enterprise account. Dedicated account manager assigned.", 
                CreatedAt = seedDate 
            }
        );

        // Seed CustomerReviews (at least 5 records, linked to customers)
        modelBuilder.Entity<CustomerReview>().HasData(
            new CustomerReview 
            { 
                Id = 1, 
                CustomerId = 1, 
                Title = "Excellent Service and Support", 
                ReviewText = "TechCorp Solutions has been working with this company for over a year. The service is outstanding, and the support team is always responsive. Highly recommend!", 
                Rating = 5, 
                ReviewerName = "John Smith", 
                ReviewDate = DateTime.Now.AddDays(-30), 
                IsPublished = true 
            },
            new CustomerReview 
            { 
                Id = 2, 
                CustomerId = 2, 
                Title = "Great Product Quality", 
                ReviewText = "We've been very satisfied with the products and services. The quality is consistently high, and delivery is always on time. Keep up the great work!", 
                Rating = 5, 
                ReviewerName = "Sarah Johnson", 
                ReviewDate = DateTime.Now.AddDays(-45), 
                IsPublished = true 
            },
            new CustomerReview 
            { 
                Id = 3, 
                CustomerId = 5, 
                Title = "Reliable Partner", 
                ReviewText = "Enterprise Systems has been a reliable partner for our business needs. The team understands our requirements and delivers accordingly. Very professional.", 
                Rating = 4, 
                ReviewerName = "David Williams", 
                ReviewDate = DateTime.Now.AddDays(-20), 
                IsPublished = true 
            },
            new CustomerReview 
            { 
                Id = 4, 
                CustomerId = 7, 
                Title = "Outstanding Enterprise Solution", 
                ReviewText = "Mega Corp International has been extremely pleased with the enterprise solution. The scalability and performance have exceeded our expectations. Excellent value for money.", 
                Rating = 5, 
                ReviewerName = "Robert Taylor", 
                ReviewDate = DateTime.Now.AddDays(-15), 
                IsPublished = true 
            },
            new CustomerReview 
            { 
                Id = 5, 
                CustomerId = 1, 
                Title = "Quick Response Time", 
                ReviewText = "What I appreciate most is the quick response time to any issues or questions. The customer service team is knowledgeable and helpful. Great experience overall.", 
                Rating = 5, 
                ReviewerName = "John Smith", 
                ReviewDate = DateTime.Now.AddDays(-10), 
                IsPublished = true 
            },
            new CustomerReview 
            { 
                Id = 6, 
                CustomerId = 2, 
                Title = "Good Value", 
                ReviewText = "The pricing is competitive and the service quality is good. We've had a positive experience working with this company. Would recommend to others.", 
                Rating = 4, 
                ReviewerName = "Sarah Johnson", 
                ReviewDate = DateTime.Now.AddDays(-25), 
                IsPublished = true 
            },
            new CustomerReview 
            { 
                Id = 7, 
                CustomerId = 5, 
                Title = "Professional Team", 
                ReviewText = "The team is very professional and easy to work with. They understand our business needs and provide tailored solutions. Very satisfied with the partnership.", 
                Rating = 5, 
                ReviewerName = "David Williams", 
                ReviewDate = DateTime.Now.AddDays(-5), 
                IsPublished = true 
            }
        );
    }
}

