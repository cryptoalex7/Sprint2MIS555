using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SkynetERP.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SkynetERP.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UserService _userService;

    public IndexModel(ILogger<IndexModel> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [BindProperty]
    public LoginModel Login { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public IActionResult OnGet()
    {
        // If user is already authenticated, redirect based on role
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToRoleBasedPage();
        }

        // Clear any existing error messages
        ErrorMessage = null;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = _userService.ValidateUser(Login.Email, Login.Password);
        
        if (user == null)
        {
            ErrorMessage = "Invalid email or password.";
            return Page();
        }

        var claimsPrincipal = _userService.CreateClaimsPrincipal(user);
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        
        _logger.LogInformation("User {Email} with role {Role} logged in successfully", user.Email, user.Role);
        
        return RedirectToRoleBasedPage(user.Role);
    }

    private IActionResult RedirectToRoleBasedPage(string? role = null)
    {
        role ??= User.FindFirst("Role")?.Value ?? User.FindFirst(ClaimTypes.Role)?.Value;

        return role switch
        {
            "Admin" => RedirectToPage("/Dashboard"), // Admin goes to dashboard
            "HR" => RedirectToPage("/HRM"),
            "Vendor" => RedirectToPage("/VendorManagement"),
            "Accountant" => RedirectToPage("/FinancialManagement"),
            "InventoryManager" => RedirectToPage("/Inventory"),
            "Customer" => RedirectToPage("/CRM"),
            "Guest" => RedirectToPage("/Privacy"),
            "User" => RedirectToPage("/Privacy"),
            _ => RedirectToPage("/Privacy") // Default fallback
        };
    }
}

public class LoginModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
}
