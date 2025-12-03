using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using SkynetERP.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize(Roles = "Admin")]
public class CRMModel : PageModel
{
    private readonly ILogger<CRMModel> _logger;
    private readonly CRMService _crmService;

    public CRMModel(ILogger<CRMModel> logger, CRMService crmService)
    {
        _logger = logger;
        _crmService = crmService;
    }

    public List<Customer> Customers { get; set; } = new();
    public List<CustomerReview> Reviews { get; set; } = new();
    public int TotalCustomers { get; set; }
    public int ActiveCustomers { get; set; }

    // Form binding properties
    [BindProperty]
    public CustomerFormModel CustomerForm { get; set; } = new();

    [BindProperty]
    public ReviewFormModel ReviewForm { get; set; } = new();

    public void OnGet()
    {
        try
        {
            Customers = _crmService.GetAllCustomers();
            Reviews = _crmService.GetAllReviews().ToList(); // Show all reviews
            TotalCustomers = Customers.Count;
            ActiveCustomers = Customers.Count(c => c.Status == "Active");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading CRM data");
            TempData["Error"] = "Error loading customer data";
        }
    }

    public IActionResult OnPostAddCustomer(
        [FromForm] string Name,
        [FromForm] string Company,
        [FromForm] string Email,
        [FromForm] string? Phone,
        [FromForm] string? Status,
        [FromForm] string? Notes)
    {
        try
        {
            _logger.LogInformation("OnPostAddCustomer called. Name: {Name}, Company: {Company}, Email: {Email}", 
                Name ?? "null", Company ?? "null", Email ?? "null");

            // Validate required fields
            if (string.IsNullOrWhiteSpace(Name))
            {
                TempData["Error"] = "Name is required";
                LoadData();
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Company))
            {
                TempData["Error"] = "Company is required";
                LoadData();
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                TempData["Error"] = "Email is required";
                LoadData();
                return Page();
            }

            var customer = new Customer
            {
                Name = Name.Trim(),
                Company = Company.Trim(),
                Email = Email.Trim(),
                Phone = Phone?.Trim(),
                Status = Status ?? "Active",
                LastContact = DateTime.Now,
                Notes = Notes?.Trim() ?? string.Empty
            };

            _crmService.AddCustomer(customer);
            _logger.LogInformation("Customer added successfully: {Name} from {Company}", customer.Name, customer.Company);
            TempData["Success"] = $"Customer '{customer.Name}' added successfully";
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding customer: {Error}", ex.Message);
            TempData["Error"] = $"Error adding customer: {ex.Message}";
            LoadData();
            return Page();
        }
    }

    public IActionResult OnPostUpdateCustomer(int id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                return Page();
            }

            var customer = new Customer
            {
                Id = id,
                Name = CustomerForm.Name,
                Company = CustomerForm.Company,
                Email = CustomerForm.Email,
                Phone = CustomerForm.Phone,
                Status = CustomerForm.Status ?? "Active",
                LastContact = CustomerForm.LastContact,
                Notes = CustomerForm.Notes ?? string.Empty
            };

            if (_crmService.UpdateCustomer(customer))
            {
                TempData["Success"] = $"Customer '{customer.Name}' updated successfully";
            }
            else
            {
                TempData["Error"] = "Customer not found";
            }
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer");
            TempData["Error"] = $"Error updating customer: {ex.Message}";
            LoadData();
            return Page();
        }
    }

    public IActionResult OnPostDeleteCustomer(int id)
    {
        try
        {
            var customer = _crmService.GetCustomerById(id);
            if (customer != null && _crmService.DeleteCustomer(id))
            {
                TempData["Success"] = $"Customer '{customer.Name}' deleted successfully";
            }
            else
            {
                TempData["Error"] = "Customer not found";
            }
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer");
            TempData["Error"] = $"Error deleting customer: {ex.Message}";
            return RedirectToPage();
        }
    }

    public IActionResult OnPostAddReview(
        [FromForm] int CustomerId,
        [FromForm] string Title,
        [FromForm] string ReviewText,
        [FromForm] int Rating,
        [FromForm] string? ReviewerName)
    {
        try
        {
            _logger.LogInformation("OnPostAddReview called. CustomerId: {CustomerId}, Title: {Title}", 
                CustomerId, Title ?? "null");

            // Validate required fields
            if (CustomerId <= 0)
            {
                TempData["Error"] = "Please select a customer";
                LoadData();
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Title))
            {
                TempData["Error"] = "Title is required";
                LoadData();
                return Page();
            }
            if (string.IsNullOrWhiteSpace(ReviewText))
            {
                TempData["Error"] = "Review text is required";
                LoadData();
                return Page();
            }
            if (Rating < 1 || Rating > 5)
            {
                TempData["Error"] = "Rating must be between 1 and 5";
                LoadData();
                return Page();
            }

            var review = new CustomerReview
            {
                CustomerId = CustomerId,
                Title = Title.Trim(),
                ReviewText = ReviewText.Trim(),
                Rating = Rating,
                ReviewerName = ReviewerName?.Trim() ?? string.Empty,
                IsPublished = true
            };

            _crmService.AddReview(review);
            _logger.LogInformation("Review added successfully for CustomerId: {CustomerId}", CustomerId);
            TempData["Success"] = "Review added successfully";
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding review: {Error}", ex.Message);
            TempData["Error"] = $"Error adding review: {ex.Message}";
            LoadData();
            return Page();
        }
    }

    public IActionResult OnPostDeleteReview(int id)
    {
        try
        {
            if (_crmService.DeleteReview(id))
            {
                TempData["Success"] = "Review deleted successfully";
            }
            else
            {
                TempData["Error"] = "Review not found";
            }
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting review");
            TempData["Error"] = $"Error deleting review: {ex.Message}";
            return RedirectToPage();
        }
    }

    private void LoadData()
    {
        Customers = _crmService.GetAllCustomers();
        Reviews = _crmService.GetAllReviews().ToList();
        TotalCustomers = Customers.Count;
        ActiveCustomers = Customers.Count(c => c.Status == "Active");
    }
}

// Form models
public class CustomerFormModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Company { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? Status { get; set; } = "Active";

    public DateTime? LastContact { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}

public class ReviewFormModel
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string ReviewText { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    [StringLength(100)]
    public string? ReviewerName { get; set; }

    public bool IsPublished { get; set; } = true;
}

