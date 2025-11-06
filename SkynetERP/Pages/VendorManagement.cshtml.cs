using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize]
public class VendorManagementModel : PageModel
{
    private readonly ILogger<VendorManagementModel> _logger;
    private readonly UserService _userService;
    private readonly VendorService _vendorService;

    public VendorManagementModel(ILogger<VendorManagementModel> logger, UserService userService, VendorService vendorService)
    {
        _logger = logger;
        _userService = userService;
        _vendorService = vendorService;
    }

    public bool CanViewSpend { get; set; }
    public string? UserRole { get; set; }
    public string? Username { get; set; }
    public List<VendorModel> Vendors { get; set; } = new();

    public void OnGet()
    {
        // Get user role from claims
        UserRole = User.FindFirst("Role")?.Value;
        Username = User.FindFirst(ClaimTypes.Name)?.Value;
        
        // Check if user can view annual spend (similar to salary check)
        CanViewSpend = _userService.CanViewSalary(UserRole);
        
        // Load vendors
        Vendors = _vendorService.GetAllVendors();
    }

    public IActionResult OnGetExportVendors()
    {
        var vendors = _vendorService.GetAllVendors();
        var userRole = User.FindFirst("Role")?.Value;
        var canViewSpend = _userService.CanViewSalary(userRole);
        
        // Generate CSV content
        var csv = new System.Text.StringBuilder();
        
        // Add header row
        csv.AppendLine("Company Name,Category,Contact Person,Address,Phone" + (canViewSpend ? ",Annual Spend" : ""));
        
        // Add data rows
        foreach (var vendor in vendors)
        {
            var spendValue = canViewSpend ? vendor.AnnualSpend.ToString() : "N/A";
            csv.AppendLine($"{EscapeCsvField(vendor.CompanyName)},{EscapeCsvField(vendor.Category)},{EscapeCsvField(vendor.ContactPerson)},{EscapeCsvField(vendor.Address)},{EscapeCsvField(vendor.Phone)}{(canViewSpend ? "," + spendValue : "")}");
        }
        
        // Return CSV file
        var fileName = $"vendors_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        return File(System.Text.Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", fileName);
    }

    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
            return string.Empty;
            
        // If field contains comma, quote, or newline, wrap in quotes and escape quotes
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
        {
            return "\"" + field.Replace("\"", "\"\"") + "\"";
        }
        
        return field;
    }

    public IActionResult OnPostAddVendor(VendorModel vendor)
    {
        if (!ModelState.IsValid)
        {
            Vendors = _vendorService.GetAllVendors();
            UserRole = User.FindFirst("Role")?.Value;
            Username = User.FindFirst(ClaimTypes.Name)?.Value;
            CanViewSpend = _userService.CanViewSalary(UserRole);
            return Page();
        }

        _vendorService.AddVendor(vendor);
        _logger.LogInformation("New vendor added: {CompanyName}", vendor.CompanyName);
        
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateVendor(VendorModel vendor)
    {
        if (!ModelState.IsValid)
        {
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Invalid vendor data" });
        }

        try
        {
            _logger.LogInformation("Update request received for vendor ID: {VendorId}", vendor.Id);
            
            var existingVendor = _vendorService.GetVendorById(vendor.Id);
            if (existingVendor != null)
            {
                _vendorService.UpdateVendor(vendor);
                _logger.LogInformation("Vendor updated: {VendorId} - {CompanyName}", vendor.Id, vendor.CompanyName);
                
                Response.ContentType = "application/json";
                return new JsonResult(new { success = true, message = "Vendor updated successfully" });
            }
            
            _logger.LogWarning("Vendor not found with ID: {VendorId}", vendor.Id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Vendor not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vendor with ID {VendorId}", vendor.Id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }

    public IActionResult OnPostDeleteVendor([FromForm] int id)
    {
        try
        {
            _logger.LogInformation("Delete request received for vendor ID: {VendorId}", id);
            
            var vendor = _vendorService.GetVendorById(id);
            if (vendor != null)
            {
                _vendorService.DeleteVendor(id);
                _logger.LogInformation("Vendor deleted: {VendorId} - {CompanyName}", id, vendor.CompanyName);
                
                Response.ContentType = "application/json";
                return new JsonResult(new { success = true, message = "Vendor deleted successfully" });
            }
            
            _logger.LogWarning("Vendor not found with ID: {VendorId}", id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Vendor not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting vendor with ID {VendorId}", id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }
}

// Vendor model for form binding
public class VendorModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Company name is required")]
    [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact person is required")]
    [StringLength(100, ErrorMessage = "Contact person cannot exceed 100 characters")]
    public string ContactPerson { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Annual spend is required")]
    [Range(0, 100000000, ErrorMessage = "Annual spend must be between 0 and 100,000,000")]
    public decimal AnnualSpend { get; set; }
}

