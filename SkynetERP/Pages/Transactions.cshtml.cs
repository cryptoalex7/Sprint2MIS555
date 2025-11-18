using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using SkynetERP.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize(Roles = "Admin")]
public class TransactionsModel : PageModel
{
    private readonly ILogger<TransactionsModel> _logger;
    private readonly FinancialService _financialService;

    public TransactionsModel(ILogger<TransactionsModel> logger, FinancialService financialService)
    {
        _logger = logger;
        _financialService = financialService;
    }

    // === PROPERTIES FOR DISPLAY ===
    /// <summary>List of transactions loaded from database</summary>
    public List<Transaction> Transactions { get; set; } = new();

    /// <summary>Available accounts for dropdown selection</summary>
    public List<Account> Accounts { get; set; } = new();

    /// <summary>Available categories for dropdown selection</summary>
    public List<Category> Categories { get; set; } = new();

    // === FILTER PROPERTIES (Persistent after submission) ===
    [BindProperty(SupportsGet = true)]
    public string? TransactionTypeFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? StatusFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? AccountIdFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? DateFromFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? DateToFilter { get; set; }

    // === CALCULATED METRICS ===
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetAmount { get; set; }
    public int TotalTransactions { get; set; }

    // === FORM BINDING PROPERTIES ===
    /// <summary>Bound transaction object for form data</summary>
    [BindProperty]
    public TransactionFormModel TransactionForm { get; set; } = new();

    // === RAZOR HANDLERS ===

    /// <summary>GET handler - Load page with filtered transactions</summary>
    public void OnGet()
    {
        try
        {
            LoadTransactions();
            LoadLookupData();
            CalculateMetrics();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading transactions");
            TempData["Error"] = "Error loading transactions";
        }
    }

    /// <summary>POST handler - Add new transaction with validation</summary>
    public IActionResult OnPostAsync()
    {
        try
        {
            // Validate form data
            if (!ModelState.IsValid)
            {
                LoadTransactions();
                LoadLookupData();
                CalculateMetrics();
                return Page();
            }

            // Map form model to Transaction entity
            var transaction = new Transaction
            {
                Description = TransactionForm.Description,
                Amount = TransactionForm.Amount,
                Type = TransactionForm.Type,
                TransactionDate = TransactionForm.TransactionDate,
                AccountId = TransactionForm.AccountId,
                CategoryId = TransactionForm.CategoryId,
                ReferenceNumber = TransactionForm.ReferenceNumber ?? string.Empty,
                Notes = TransactionForm.Notes ?? string.Empty,
                Status = "Pending", // Default status
                CreatedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System",
                CreatedAt = DateTime.Now
            };

            // Add transaction to database
            _logger.LogInformation("Adding transaction: {Description} - ${Amount}", 
                transaction.Description, transaction.Amount);
            _financialService.AddTransaction(transaction);

            TempData["Success"] = $"Transaction added successfully: ${TransactionForm.Amount:N2}";

            // Clear form and refresh data
            TransactionForm = new();
            LoadTransactions();
            LoadLookupData();
            CalculateMetrics();

            // Preserve filter values after submission
            return RedirectToPage(new
            {
                TransactionTypeFilter,
                StatusFilter,
                AccountIdFilter,
                DateFromFilter,
                DateToFilter
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding transaction");
            TempData["Error"] = $"Error adding transaction: {ex.Message}";
            LoadTransactions();
            LoadLookupData();
            return Page();
        }
    }

    /// <summary>POST handler - Update existing transaction</summary>
    public IActionResult OnPostUpdateAsync(int id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                LoadTransactions();
                LoadLookupData();
                return Page();
            }

            var transaction = _financialService.GetTransactionById(id);
            if (transaction == null)
            {
                TempData["Error"] = "Transaction not found";
                return Page();
            }

            // Update properties
            transaction.Description = TransactionForm.Description;
            transaction.Amount = TransactionForm.Amount;
            transaction.Type = TransactionForm.Type;
            transaction.TransactionDate = TransactionForm.TransactionDate;
            transaction.AccountId = TransactionForm.AccountId;
            transaction.CategoryId = TransactionForm.CategoryId;
            transaction.ReferenceNumber = TransactionForm.ReferenceNumber ?? string.Empty;
            transaction.Notes = TransactionForm.Notes ?? string.Empty;

            _logger.LogInformation("Updating transaction ID: {Id}", id);
            _financialService.UpdateTransaction(transaction);

            TempData["Success"] = "Transaction updated successfully";
            TransactionForm = new();

            LoadTransactions();
            LoadLookupData();
            CalculateMetrics();

            return RedirectToPage(new
            {
                TransactionTypeFilter,
                StatusFilter,
                AccountIdFilter,
                DateFromFilter,
                DateToFilter
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating transaction");
            TempData["Error"] = $"Error updating transaction: {ex.Message}";
            return Page();
        }
    }

    /// <summary>POST handler - Delete transaction</summary>
    public IActionResult OnPostDeleteAsync(int id)
    {
        try
        {
            var transaction = _financialService.GetTransactionById(id);
            if (transaction == null)
            {
                return new JsonResult(new { success = false, message = "Transaction not found" });
            }

            _logger.LogInformation("Deleting transaction ID: {Id}", id);
            _financialService.DeleteTransaction(id);

            return new JsonResult(new { success = true, message = "Transaction deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting transaction");
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    /// <summary>POST handler - Recalculate transactions (after filtering)</summary>
    public IActionResult OnPostRecalculateAsync()
    {
        try
        {
            LoadTransactions();
            CalculateMetrics();

            return new JsonResult(new
            {
                success = true,
                totalRevenue = TotalRevenue,
                totalExpense = TotalExpense,
                netAmount = NetAmount,
                transactionCount = TotalTransactions
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error recalculating metrics");
            return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // === PRIVATE HELPER METHODS ===

    /// <summary>Load transactions from database with applied filters</summary>
    private void LoadTransactions()
    {
        var query = _financialService.GetAllTransactions().AsQueryable();

        // Apply Type filter
        if (!string.IsNullOrEmpty(TransactionTypeFilter))
        {
            query = query.Where(t => t.Type == TransactionTypeFilter);
        }

        // Apply Status filter
        if (!string.IsNullOrEmpty(StatusFilter))
        {
            query = query.Where(t => t.Status == StatusFilter);
        }

        // Apply Account filter
        if (AccountIdFilter.HasValue)
        {
            query = query.Where(t => t.AccountId == AccountIdFilter.Value);
        }

        // Apply Date Range filter
        if (DateFromFilter.HasValue)
        {
            query = query.Where(t => t.TransactionDate >= DateFromFilter.Value);
        }

        if (DateToFilter.HasValue)
        {
            // Include entire day
            query = query.Where(t => t.TransactionDate <= DateToFilter.Value.AddDays(1));
        }

        Transactions = query.OrderByDescending(t => t.TransactionDate).ToList();
    }

    /// <summary>Load lookup data (accounts, categories) for dropdowns</summary>
    private void LoadLookupData()
    {
        try
        {
            Accounts = _financialService.GetAllAccounts() ?? new List<Account>();
            Categories = _financialService.GetAllCategories() ?? new List<Category>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading lookup data");
        }
    }

    /// <summary>Calculate financial metrics from filtered transactions</summary>
    private void CalculateMetrics()
    {
        TotalRevenue = Transactions
            .Where(t => t.Type == "Revenue" && t.Status == "Completed")
            .Sum(t => t.Amount);

        TotalExpense = Transactions
            .Where(t => t.Type == "Expense" && t.Status == "Completed")
            .Sum(t => t.Amount);

        NetAmount = TotalRevenue - TotalExpense;
        TotalTransactions = Transactions.Count;

        _logger.LogInformation("Metrics calculated - Revenue: ${Revenue}, Expense: ${Expense}, Net: ${Net}",
            TotalRevenue, TotalExpense, NetAmount);
    }
}

/// <summary>Form binding model for transaction submission</summary>
public class TransactionFormModel
{
    [Required(ErrorMessage = "Description is required")]
    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Transaction type is required")]
    public string Type { get; set; } = string.Empty; // Revenue or Expense

    [Required(ErrorMessage = "Transaction date is required")]
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Account is required")]
    public int AccountId { get; set; }

    public int? CategoryId { get; set; }

    [StringLength(100)]
    public string? ReferenceNumber { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}
