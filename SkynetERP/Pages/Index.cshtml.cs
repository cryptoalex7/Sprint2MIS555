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
        // If user is already authenticated, redirect to HRM
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToPage("/HRM");
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

        var user = _userService.ValidateUser(Login.Username, Login.Password);
        
        if (user == null)
        {
            ErrorMessage = "Invalid username or password.";
            return Page();
        }

        var claimsPrincipal = _userService.CreateClaimsPrincipal(user);
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        
        _logger.LogInformation("User {Username} with role {Role} logged in successfully", user.Username, user.Role);
        
        return RedirectToPage("/HRM");
    }
}

public class LoginModel
{
    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
}
