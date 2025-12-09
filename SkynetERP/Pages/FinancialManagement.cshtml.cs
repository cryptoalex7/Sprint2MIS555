using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using SkynetERP.Models;
using System.ComponentModel.DataAnnotations;

namespace SkynetERP.Pages;

[Authorize(Roles = "Admin,Accountant")]
public class FinancialManagementModel : PageModel
{
    private readonly ILogger<FinancialManagementModel> _logger;
    private readonly FinancialService _financialService;

    public FinancialManagementModel(ILogger<FinancialManagementModel> logger, FinancialService financialService)
    {
        _logger = logger;
        _financialService = financialService;
    }

    // Overview Card Metrics
    public int ActiveAccountCount { get; set; }
    public int CustomerCount { get; set; }
    public int VendorCount { get; set; }
    public int BothCount { get; set; }
    public decimal TotalAR { get; set; }
    public decimal TotalAP { get; set; }
    public decimal OpenBalances { get; set; }
    public decimal TotalInflow { get; set; }
    public decimal TotalOutflow { get; set; }
    public int JournalEntryCount { get; set; }
    public int JournalLineCount { get; set; }
    public int TaxRateCount { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetBalance { get; set; }

    // Filters
    [BindProperty(SupportsGet = true)]
    public string? PartnerTypeFilter { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? InvoiceTypeFilter { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? PaymentTypeFilter { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? Section { get; set; }

    // Data Lists (8 tables)
    public List<Account> Accounts { get; set; } = new();
    public List<Partner> Partners { get; set; } = new();
    public List<TaxRate> TaxRates { get; set; } = new();
    public List<Invoice> Invoices { get; set; } = new();
    public List<InvoiceLine> InvoiceLines { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();
    public List<JournalEntry> JournalEntries { get; set; } = new();
    public List<JournalLine> JournalLines { get; set; } = new();

    public void OnGet()
    {
        try
        {
            LoadData();
            CalculateMetrics();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading financial data");
            TempData["Error"] = $"Error loading data: {ex.Message}";
            // Initialize empty lists to prevent null reference errors
            Accounts = new List<Account>();
            Partners = new List<Partner>();
            TaxRates = new List<TaxRate>();
            Invoices = new List<Invoice>();
            InvoiceLines = new List<InvoiceLine>();
            Payments = new List<Payment>();
            JournalEntries = new List<JournalEntry>();
            JournalLines = new List<JournalLine>();
        }
    }

    private void LoadData()
    {
        Accounts = _financialService.GetAllAccounts() ?? new List<Account>();
        Partners = _financialService.GetPartnersByType(PartnerTypeFilter) ?? new List<Partner>();
        TaxRates = _financialService.GetAllTaxRates() ?? new List<TaxRate>();
        Invoices = _financialService.GetInvoicesByType(InvoiceTypeFilter) ?? new List<Invoice>();
        InvoiceLines = _financialService.GetAllInvoiceLines() ?? new List<InvoiceLine>();
        Payments = _financialService.GetPaymentsByType(PaymentTypeFilter) ?? new List<Payment>();
        JournalEntries = _financialService.GetAllJournalEntries() ?? new List<JournalEntry>();
        JournalLines = _financialService.GetAllJournalLines() ?? new List<JournalLine>();
    }

    private void CalculateMetrics()
    {
        ActiveAccountCount = _financialService.GetActiveAccountCount();
        CustomerCount = _financialService.GetCustomerCount();
        VendorCount = _financialService.GetVendorCount();
        BothCount = _financialService.GetBothCount();
        TotalAR = _financialService.GetTotalAR();
        TotalAP = _financialService.GetTotalAP();
        OpenBalances = _financialService.GetOpenBalances();
        TotalInflow = _financialService.GetTotalInflow();
        TotalOutflow = _financialService.GetTotalOutflow();
        JournalEntryCount = _financialService.GetJournalEntryCount();
        JournalLineCount = _financialService.GetJournalLineCount();
        TaxRateCount = _financialService.GetTaxRateCount();
        TotalRevenue = _financialService.GetTotalRevenue();
        TotalExpense = _financialService.GetTotalExpense();
        NetBalance = TotalRevenue - TotalExpense;
    }

    // Account CRUD
    public IActionResult OnPostAddAccount([FromForm] Account account)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.AddAccount(account);
            TempData["Success"] = "Account added successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error adding account: {ex.Message}";
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateAccount([FromForm] Account account, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.UpdateAccount(account);
            TempData["Success"] = "Account updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating account: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeleteAccount(int id)
    {
        try
        {
            _financialService.DeleteAccount(id);
            return new JsonResult(new { success = true, message = "Account deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // Partner CRUD
    public IActionResult OnPostAddPartner([FromForm] Partner partner)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.AddPartner(partner);
            TempData["Success"] = "Partner added successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error adding partner: {ex.Message}";
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdatePartner([FromForm] Partner partner, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.UpdatePartner(partner);
            TempData["Success"] = "Partner updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating partner: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeletePartner(int id)
    {
        try
        {
            _financialService.DeletePartner(id);
            return new JsonResult(new { success = true, message = "Partner deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // TaxRate CRUD
    public IActionResult OnPostAddTaxRate([FromForm] TaxRate taxRate)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.AddTaxRate(taxRate);
            TempData["Success"] = "Tax rate added successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error adding tax rate: {ex.Message}";
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateTaxRate([FromForm] TaxRate taxRate, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.UpdateTaxRate(taxRate);
            TempData["Success"] = "Tax rate updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating tax rate: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeleteTaxRate(int id)
    {
        try
        {
            _financialService.DeleteTaxRate(id);
            return new JsonResult(new { success = true, message = "Tax rate deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // Invoice CRUD
    public IActionResult OnPostAddInvoice([FromForm] Invoice invoice)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            invoice.Balance = invoice.Amount - invoice.PaidAmount;
            _financialService.AddInvoice(invoice);
            TempData["Success"] = "Invoice added successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error adding invoice: {ex.Message}";
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateInvoice([FromForm] Invoice invoice, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            invoice.Balance = invoice.Amount - invoice.PaidAmount;
            _financialService.UpdateInvoice(invoice);
            TempData["Success"] = "Invoice updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating invoice: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeleteInvoice(int id)
    {
        try
        {
            _financialService.DeleteInvoice(id);
            return new JsonResult(new { success = true, message = "Invoice deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // InvoiceLine CRUD
    public IActionResult OnPostAddInvoiceLine([FromForm] InvoiceLine invoiceLine)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.AddInvoiceLine(invoiceLine);
            return new JsonResult(new { success = true, message = "Invoice line added successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    public IActionResult OnPostUpdateInvoiceLine([FromForm] InvoiceLine invoiceLine, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.UpdateInvoiceLine(invoiceLine);
            TempData["Success"] = "Invoice line updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating invoice line: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeleteInvoiceLine(int id)
    {
        try
        {
            _financialService.DeleteInvoiceLine(id);
            return new JsonResult(new { success = true, message = "Invoice line deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // Payment CRUD
    public IActionResult OnPostAddPayment([FromForm] Payment payment)
    {
        if (!ModelState.IsValid)
        {
            LoadData();
            CalculateMetrics();
            return Page();
        }
        try
        {
            _financialService.AddPayment(payment);
            TempData["Success"] = "Payment added successfully";
            // Clear any filters to ensure all data is shown
            return RedirectToPage(new { PartnerTypeFilter = (string?)null, InvoiceTypeFilter = (string?)null, PaymentTypeFilter = (string?)null, section = (string?)null });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding payment: {Message}", ex.Message);
            TempData["Error"] = $"Error adding payment: {ex.Message}";
            // Still redirect to reload data
            return RedirectToPage(new { PartnerTypeFilter = (string?)null, InvoiceTypeFilter = (string?)null, PaymentTypeFilter = (string?)null });
        }
    }

    public IActionResult OnPostUpdatePayment([FromForm] Payment payment, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.UpdatePayment(payment);
            TempData["Success"] = "Payment updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating payment: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeletePayment(int id)
    {
        try
        {
            _financialService.DeletePayment(id);
            return new JsonResult(new { success = true, message = "Payment deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // JournalEntry CRUD
    public IActionResult OnPostAddJournalEntry([FromForm] JournalEntry journalEntry)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.AddJournalEntry(journalEntry);
            TempData["Success"] = "Journal entry added successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error adding journal entry: {ex.Message}";
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateJournalEntry([FromForm] JournalEntry journalEntry, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.UpdateJournalEntry(journalEntry);
            TempData["Success"] = "Journal entry updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating journal entry: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeleteJournalEntry(int id)
    {
        try
        {
            _financialService.DeleteJournalEntry(id);
            return new JsonResult(new { success = true, message = "Journal entry deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // JournalLine CRUD
    public IActionResult OnPostAddJournalLine([FromForm] JournalLine journalLine)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.AddJournalLine(journalLine);
            return new JsonResult(new { success = true, message = "Journal line added successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    public IActionResult OnPostUpdateJournalLine([FromForm] JournalLine journalLine, [FromForm] string? section)
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            _financialService.UpdateJournalLine(journalLine);
            TempData["Success"] = "Journal line updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating journal line: {ex.Message}";
        }
        return RedirectToPage(new { section });
    }

    public IActionResult OnPostDeleteJournalLine(int id)
    {
        try
        {
            _financialService.DeleteJournalLine(id);
            return new JsonResult(new { success = true, message = "Journal line deleted successfully" });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }
}
