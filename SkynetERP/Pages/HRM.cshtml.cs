using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize]
public class HRMModel : PageModel
{
    private readonly ILogger<HRMModel> _logger;
    private readonly UserService _userService;

    public HRMModel(ILogger<HRMModel> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public bool CanViewSalary { get; set; }
    public string? UserRole { get; set; }
    public string? Username { get; set; }

    public void OnGet()
    {
        // Get user role from claims
        UserRole = User.FindFirst("Role")?.Value;
        Username = User.FindFirst(ClaimTypes.Name)?.Value;
        
        // Check if user can view salary
        CanViewSalary = _userService.CanViewSalary(UserRole);
    }

    public IActionResult OnPostAddEmployee(EmployeeModel employee)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Add employee to database
        _logger.LogInformation("New employee added: {FirstName} {LastName}", employee.FirstName, employee.LastName);
        
        return RedirectToPage();
    }
}

// Employee model for form binding
public class EmployeeModel
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    public string Department { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role is required")]
    [StringLength(100, ErrorMessage = "Role cannot exceed 100 characters")]
    public string Role { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Salary is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
    public decimal Salary { get; set; }
}
