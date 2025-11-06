using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize(Roles = "Admin,Vendor")]
public class VendorManagementModel : PageModel
{
    private readonly ILogger<VendorManagementModel> _logger;
    private readonly UserService _userService;
    private readonly VendorService _vendorService;
    private readonly DeliveryService _deliveryService;
    private readonly IWebHostEnvironment _environment;

    public VendorManagementModel(ILogger<VendorManagementModel> logger, UserService userService, VendorService vendorService, DeliveryService deliveryService, IWebHostEnvironment environment)
    {
        _logger = logger;
        _userService = userService;
        _vendorService = vendorService;
        _deliveryService = deliveryService;
        _environment = environment;
    }

    public bool CanViewSpend { get; set; }
    public string? UserRole { get; set; }
    public string? Username { get; set; }
    public List<VendorModel> Vendors { get; set; } = new();
    public List<DeliveryModel> Deliveries { get; set; } = new();
    public int TotalVendors { get; set; }
    public int TotalCategories { get; set; }
    public decimal AverageAnnualSpend { get; set; }
    public decimal TotalAnnualSpend { get; set; }

    public void OnGet()
    {
        // Get user role from claims
        UserRole = User.FindFirst("Role")?.Value;
        Username = User.FindFirst(ClaimTypes.Name)?.Value;
        
        // Check if user can view annual spend (similar to salary check)
        CanViewSpend = _userService.CanViewSalary(UserRole);
        
        // Load vendors
        Vendors = _vendorService.GetAllVendors();
        
        // Load deliveries
        Deliveries = _deliveryService.GetAllDeliveries();
        
        // Calculate statistics
        TotalVendors = Vendors.Count;
        TotalCategories = Vendors.Select(v => v.Category).Distinct().Count();
        
        if (Vendors.Any() && CanViewSpend)
        {
            AverageAnnualSpend = Vendors.Average(v => v.AnnualSpend);
            TotalAnnualSpend = Vendors.Sum(v => v.AnnualSpend);
        }
        else if (Vendors.Any())
        {
            // If user can't view spend, show 0
            AverageAnnualSpend = 0;
            TotalAnnualSpend = 0;
        }
        else
        {
            AverageAnnualSpend = 0;
            TotalAnnualSpend = 0;
        }
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

    public async Task<IActionResult> OnPostConfirmDelivery([FromForm] DeliveryModel delivery, IFormFile? deliveryPhoto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                Response.ContentType = "application/json";
                return new JsonResult(new { success = false, message = "Invalid delivery data" });
            }

            string? photoPath = null;

            // Handle file upload
            if (deliveryPhoto != null && deliveryPhoto.Length > 0)
            {
                // Validate file type
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
                var fileExtension = Path.GetExtension(deliveryPhoto.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    Response.ContentType = "application/json";
                    return new JsonResult(new { success = false, message = "Invalid file type. Only JPG, PNG, GIF, and PDF files are allowed." });
                }

                // Validate file size (max 10MB)
                if (deliveryPhoto.Length > 10 * 1024 * 1024)
                {
                    Response.ContentType = "application/json";
                    return new JsonResult(new { success = false, message = "File size exceeds 10MB limit." });
                }

                // Create uploads directory if it doesn't exist
                var uploadsDir = Path.Combine(_environment.WebRootPath, "uploads", "deliveries");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsDir, fileName);
                
                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await deliveryPhoto.CopyToAsync(stream);
                }

                // Store relative path
                photoPath = $"/uploads/deliveries/{fileName}";
            }

            // Set created by
            delivery.CreatedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";

            // Add delivery
            _deliveryService.AddDelivery(delivery, photoPath);
            _logger.LogInformation("Delivery confirmed: {DeliveryNumber} for vendor {VendorId}", delivery.DeliveryNumber, delivery.VendorId);

            Response.ContentType = "application/json";
            return new JsonResult(new { success = true, message = "Delivery confirmed successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error confirming delivery");
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

// Delivery model for form binding
public class DeliveryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vendor is required")]
    public int VendorId { get; set; }

    public string VendorName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Delivery number is required")]
    [StringLength(200, ErrorMessage = "Delivery number cannot exceed 200 characters")]
    public string DeliveryNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Delivery date is required")]
    public DateTime DeliveryDate { get; set; } = DateTime.Now;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    [StringLength(100)]
    public string Status { get; set; } = "Pending";

    public string PhotoPath { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = string.Empty;
}

