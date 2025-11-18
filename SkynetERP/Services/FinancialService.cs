using Microsoft.EntityFrameworkCore;
using SkynetERP.Data;
using SkynetERP.Models;

namespace SkynetERP.Services;

public class FinancialService
{
    private readonly ApplicationDbContext _context;

    public FinancialService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Account methods
    public List<Account> GetAllAccounts() => _context.Accounts.Where(a => a.IsActive).ToList();
    public Account? GetAccountById(int id) => _context.Accounts.Find(id);
    public void AddAccount(Account account)
    {
        account.CreatedAt = DateTime.Now;
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }
    public void UpdateAccount(Account account)
    {
        _context.Accounts.Update(account);
        _context.SaveChanges();
    }
    public void DeleteAccount(int id)
    {
        var account = _context.Accounts.Find(id);
        if (account != null)
        {
            account.IsActive = false;
            _context.SaveChanges();
        }
    }
    public int GetActiveAccountCount() => _context.Accounts.Count(a => a.IsActive);

    // Partner methods
    public List<Partner> GetAllPartners() => _context.Partners.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
    public List<Partner> GetPartnersByType(string? type)
    {
        var query = _context.Partners.Where(p => p.IsActive).AsQueryable();
        if (!string.IsNullOrEmpty(type))
            query = query.Where(p => p.Type == type || p.Type == "Both");
        return query.OrderBy(p => p.Name).ToList();
    }
    public Partner? GetPartnerById(int id) => _context.Partners.Find(id);
    public void AddPartner(Partner partner)
    {
        partner.CreatedAt = DateTime.Now;
        _context.Partners.Add(partner);
        _context.SaveChanges();
    }
    public void UpdatePartner(Partner partner)
    {
        _context.Partners.Update(partner);
        _context.SaveChanges();
    }
    public void DeletePartner(int id)
    {
        var partner = _context.Partners.Find(id);
        if (partner != null)
        {
            partner.IsActive = false;
            _context.SaveChanges();
        }
    }
    public int GetCustomerCount() => _context.Partners.Count(p => p.IsActive && (p.Type == "Customer" || p.Type == "Both"));
    public int GetVendorCount() => _context.Partners.Count(p => p.IsActive && (p.Type == "Vendor" || p.Type == "Both"));
    public int GetBothCount() => _context.Partners.Count(p => p.IsActive && p.Type == "Both");

    // TaxRate methods
    public List<TaxRate> GetAllTaxRates() => _context.TaxRates.Where(t => t.IsActive).OrderBy(t => t.Name).ToList();
    public TaxRate? GetTaxRateById(int id) => _context.TaxRates.Find(id);
    public void AddTaxRate(TaxRate taxRate)
    {
        taxRate.CreatedAt = DateTime.Now;
        _context.TaxRates.Add(taxRate);
        _context.SaveChanges();
    }
    public void UpdateTaxRate(TaxRate taxRate)
    {
        _context.TaxRates.Update(taxRate);
        _context.SaveChanges();
    }
    public void DeleteTaxRate(int id)
    {
        var taxRate = _context.TaxRates.Find(id);
        if (taxRate != null)
        {
            taxRate.IsActive = false;
            _context.SaveChanges();
        }
    }
    public int GetTaxRateCount() => _context.TaxRates.Count(t => t.IsActive);

    // Invoice methods
    public List<Invoice> GetAllInvoices() => _context.Invoices
        .Include(i => i.Partner)
        .Include(i => i.InvoiceLines)
        .OrderByDescending(i => i.InvoiceDate)
        .ToList();
    public List<Invoice> GetInvoicesByType(string? type)
    {
        var query = _context.Invoices.Include(i => i.Partner).Include(i => i.InvoiceLines).AsQueryable();
        if (!string.IsNullOrEmpty(type))
            query = query.Where(i => i.InvoiceType == type);
        return query.OrderByDescending(i => i.InvoiceDate).ToList();
    }
    public Invoice? GetInvoiceById(int id) => _context.Invoices
        .Include(i => i.Partner)
        .Include(i => i.InvoiceLines)
        .FirstOrDefault(i => i.Id == id);
    public void AddInvoice(Invoice invoice)
    {
        invoice.CreatedAt = DateTime.Now;
        invoice.Balance = invoice.Amount - invoice.PaidAmount;
        _context.Invoices.Add(invoice);
        _context.SaveChanges();
    }
    public void UpdateInvoice(Invoice invoice)
    {
        invoice.Balance = invoice.Amount - invoice.PaidAmount;
        _context.Invoices.Update(invoice);
        _context.SaveChanges();
    }
    public void DeleteInvoice(int id)
    {
        var invoice = _context.Invoices.Find(id);
        if (invoice != null)
        {
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
        }
    }
    public decimal GetTotalAR() => _context.Invoices.Where(i => i.InvoiceType == "AR").Sum(i => (decimal?)i.Balance) ?? 0;
    public decimal GetTotalAP() => _context.Invoices.Where(i => i.InvoiceType == "AP").Sum(i => (decimal?)i.Balance) ?? 0;
    
    // Revenue and Expense calculations
    public decimal GetTotalRevenue() => _context.Invoices.Where(i => i.InvoiceType == "AR").Sum(i => (decimal?)i.Amount) ?? 0;
    public decimal GetTotalExpense() => _context.Invoices.Where(i => i.InvoiceType == "AP").Sum(i => (decimal?)i.Amount) ?? 0;
    public decimal GetOpenBalances() => _context.Invoices.Sum(i => (decimal?)i.Balance) ?? 0;

    // InvoiceLine methods
    public List<InvoiceLine> GetAllInvoiceLines() => _context.InvoiceLines
        .Include(il => il.Invoice)
        .OrderByDescending(il => il.CreatedAt)
        .ToList();
    public List<InvoiceLine> GetInvoiceLinesByInvoice(int invoiceId) => _context.InvoiceLines
        .Where(il => il.InvoiceId == invoiceId)
        .OrderBy(il => il.Id)
        .ToList();
    public InvoiceLine? GetInvoiceLineById(int id) => _context.InvoiceLines
        .Include(il => il.Invoice)
        .FirstOrDefault(il => il.Id == id);
    public void AddInvoiceLine(InvoiceLine invoiceLine)
    {
        invoiceLine.CreatedAt = DateTime.Now;
        invoiceLine.LineTotal = (invoiceLine.Quantity * invoiceLine.UnitPrice) * (1 + invoiceLine.TaxRate / 100);
        _context.InvoiceLines.Add(invoiceLine);
        
        // Update invoice total
        var invoice = _context.Invoices.Find(invoiceLine.InvoiceId);
        if (invoice != null)
        {
            invoice.Amount = _context.InvoiceLines.Where(il => il.InvoiceId == invoice.Id).Sum(il => il.LineTotal);
            invoice.Balance = invoice.Amount - invoice.PaidAmount;
            _context.SaveChanges();
        }
        else
        {
            _context.SaveChanges();
        }
    }
    public void UpdateInvoiceLine(InvoiceLine invoiceLine)
    {
        invoiceLine.LineTotal = (invoiceLine.Quantity * invoiceLine.UnitPrice) * (1 + invoiceLine.TaxRate / 100);
        _context.InvoiceLines.Update(invoiceLine);
        
        // Update invoice total
        var invoice = _context.Invoices.Find(invoiceLine.InvoiceId);
        if (invoice != null)
        {
            invoice.Amount = _context.InvoiceLines.Where(il => il.InvoiceId == invoice.Id).Sum(il => il.LineTotal);
            invoice.Balance = invoice.Amount - invoice.PaidAmount;
            _context.SaveChanges();
        }
        else
        {
            _context.SaveChanges();
        }
    }
    public void DeleteInvoiceLine(int id)
    {
        var invoiceLine = _context.InvoiceLines.Find(id);
        if (invoiceLine != null)
        {
            var invoiceId = invoiceLine.InvoiceId;
            _context.InvoiceLines.Remove(invoiceLine);
            
            // Update invoice total
            var invoice = _context.Invoices.Find(invoiceId);
            if (invoice != null)
            {
                invoice.Amount = _context.InvoiceLines.Where(il => il.InvoiceId == invoice.Id).Sum(il => (decimal?)il.LineTotal) ?? 0;
                invoice.Balance = invoice.Amount - invoice.PaidAmount;
            }
            _context.SaveChanges();
        }
    }

    // Payment methods
    public List<Payment> GetAllPayments() => _context.Payments
        .Include(p => p.Account)
        .Include(p => p.Invoice)
        .Include(p => p.Partner)
        .OrderByDescending(p => p.PaymentDate)
        .ToList();
    public List<Payment> GetPaymentsByType(string? type)
    {
        var query = _context.Payments.Include(p => p.Account).Include(p => p.Invoice).Include(p => p.Partner).AsQueryable();
        if (!string.IsNullOrEmpty(type))
            query = query.Where(p => p.PaymentType == type);
        return query.OrderByDescending(p => p.PaymentDate).ToList();
    }
    public Payment? GetPaymentById(int id) => _context.Payments
        .Include(p => p.Account)
        .Include(p => p.Invoice)
        .Include(p => p.Partner)
        .FirstOrDefault(p => p.Id == id);
    public void AddPayment(Payment payment)
    {
        payment.CreatedAt = DateTime.Now;
        _context.Payments.Add(payment);
        
        // Update invoice paid amount if linked
        if (payment.InvoiceId.HasValue)
        {
            var invoice = _context.Invoices.Find(payment.InvoiceId.Value);
            if (invoice != null)
            {
                invoice.PaidAmount = _context.Payments.Where(p => p.InvoiceId == invoice.Id && p.Status == "Completed").Sum(p => (decimal?)p.Amount) ?? 0;
                invoice.Balance = invoice.Amount - invoice.PaidAmount;
            }
        }
        _context.SaveChanges();
    }
    public void UpdatePayment(Payment payment)
    {
        _context.Payments.Update(payment);
        
        // Update invoice paid amount if linked
        if (payment.InvoiceId.HasValue)
        {
            var invoice = _context.Invoices.Find(payment.InvoiceId.Value);
            if (invoice != null)
            {
                invoice.PaidAmount = _context.Payments.Where(p => p.InvoiceId == invoice.Id && p.Status == "Completed").Sum(p => (decimal?)p.Amount) ?? 0;
                invoice.Balance = invoice.Amount - invoice.PaidAmount;
            }
        }
        _context.SaveChanges();
    }
    public void DeletePayment(int id)
    {
        var payment = _context.Payments.Find(id);
        if (payment != null)
        {
            var invoiceId = payment.InvoiceId;
            _context.Payments.Remove(payment);
            
            // Update invoice paid amount if linked
            if (invoiceId.HasValue)
            {
                var invoice = _context.Invoices.Find(invoiceId.Value);
                if (invoice != null)
                {
                    invoice.PaidAmount = _context.Payments.Where(p => p.InvoiceId == invoice.Id && p.Status == "Completed").Sum(p => (decimal?)p.Amount) ?? 0;
                    invoice.Balance = invoice.Amount - invoice.PaidAmount;
                }
            }
            _context.SaveChanges();
        }
    }
    public decimal GetTotalInflow() => _context.Payments.Where(p => p.PaymentType == "Inflow" && p.Status == "Completed").Sum(p => (decimal?)p.Amount) ?? 0;
    public decimal GetTotalOutflow() => _context.Payments.Where(p => p.PaymentType == "Outflow" && p.Status == "Completed").Sum(p => (decimal?)p.Amount) ?? 0;

    // JournalEntry methods
    public List<JournalEntry> GetAllJournalEntries() => _context.JournalEntries
        .Include(je => je.JournalLines)
        .OrderByDescending(je => je.EntryDate)
        .ToList();
    public JournalEntry? GetJournalEntryById(int id) => _context.JournalEntries
        .Include(je => je.JournalLines)
        .FirstOrDefault(je => je.Id == id);
    public void AddJournalEntry(JournalEntry journalEntry)
    {
        journalEntry.CreatedAt = DateTime.Now;
        _context.JournalEntries.Add(journalEntry);
        _context.SaveChanges();
    }
    public void UpdateJournalEntry(JournalEntry journalEntry)
    {
        _context.JournalEntries.Update(journalEntry);
        _context.SaveChanges();
    }
    public void DeleteJournalEntry(int id)
    {
        var journalEntry = _context.JournalEntries.Find(id);
        if (journalEntry != null)
        {
            _context.JournalEntries.Remove(journalEntry);
            _context.SaveChanges();
        }
    }
    public int GetJournalEntryCount() => _context.JournalEntries.Count();

    // JournalLine methods
    public List<JournalLine> GetAllJournalLines() => _context.JournalLines
        .Include(jl => jl.JournalEntry)
        .OrderByDescending(jl => jl.CreatedAt)
        .ToList();
    public List<JournalLine> GetJournalLinesByEntry(int journalEntryId) => _context.JournalLines
        .Where(jl => jl.JournalEntryId == journalEntryId)
        .OrderBy(jl => jl.Id)
        .ToList();
    public JournalLine? GetJournalLineById(int id) => _context.JournalLines
        .Include(jl => jl.JournalEntry)
        .FirstOrDefault(jl => jl.Id == id);
    public void AddJournalLine(JournalLine journalLine)
    {
        journalLine.CreatedAt = DateTime.Now;
        _context.JournalLines.Add(journalLine);
        _context.SaveChanges();
    }
    public void UpdateJournalLine(JournalLine journalLine)
    {
        _context.JournalLines.Update(journalLine);
        _context.SaveChanges();
    }
    public void DeleteJournalLine(int id)
    {
        var journalLine = _context.JournalLines.Find(id);
        if (journalLine != null)
        {
            _context.JournalLines.Remove(journalLine);
            _context.SaveChanges();
        }
    }
    public int GetJournalLineCount() => _context.JournalLines.Count();

    // Transaction methods
    public List<Transaction> GetAllTransactions() => _context.Transactions
        .Include(t => t.Account)
        .Include(t => t.Category)
        .OrderByDescending(t => t.TransactionDate)
        .ToList();

    public List<Transaction> GetTransactionsByType(string type) => _context.Transactions
        .Include(t => t.Account)
        .Include(t => t.Category)
        .Where(t => t.Type == type)
        .OrderByDescending(t => t.TransactionDate)
        .ToList();

    public List<Transaction> GetTransactionsByDateRange(DateTime from, DateTime to) => _context.Transactions
        .Include(t => t.Account)
        .Include(t => t.Category)
        .Where(t => t.TransactionDate >= from && t.TransactionDate <= to)
        .OrderByDescending(t => t.TransactionDate)
        .ToList();

    public List<Transaction> GetTransactionsByAccount(int accountId) => _context.Transactions
        .Include(t => t.Account)
        .Include(t => t.Category)
        .Where(t => t.AccountId == accountId)
        .OrderByDescending(t => t.TransactionDate)
        .ToList();

    public Transaction? GetTransactionById(int id) => _context.Transactions
        .Include(t => t.Account)
        .Include(t => t.Category)
        .FirstOrDefault(t => t.Id == id);

    public void AddTransaction(Transaction transaction)
    {
        transaction.CreatedAt = DateTime.Now;
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
    }

    public void UpdateTransaction(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        _context.SaveChanges();
    }

    public void DeleteTransaction(int id)
    {
        var transaction = _context.Transactions.Find(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }
    }

    public decimal GetTotalTransactionAmount(string? type = null, DateTime? from = null, DateTime? to = null)
    {
        var query = _context.Transactions.AsQueryable();

        if (!string.IsNullOrEmpty(type))
            query = query.Where(t => t.Type == type);

        if (from.HasValue)
            query = query.Where(t => t.TransactionDate >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.TransactionDate <= to.Value);

        return query.Sum(t => (decimal?)t.Amount) ?? 0;
    }

    // Category methods
    public List<Category>? GetAllCategories() => _context.Categories.Where(c => c.IsActive).ToList();
    public Category? GetCategoryById(int id) => _context.Categories.Find(id);
}
