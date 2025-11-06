using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkynetERP.Data;
using SkynetERP.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SkynetERP.Pages;

public class RegisterModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(ApplicationDbContext context, ILogger<RegisterModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters")]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Please confirm your password")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Role is required")]
    [MaxLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
    public string Role { get; set; } = "User"; // Default to User

    public string ErrorMessage { get; set; } = string.Empty;
    public string SuccessMessage { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        // If user is already authenticated, redirect to HRM
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToPage("/HRM");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            // Check if email already exists
            var existingUserByEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (existingUserByEmail != null)
            {
                ErrorMessage = "An account with this email already exists.";
                return Page();
            }

            // Check if username already exists
            var existingUserByUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == Username);

            if (existingUserByUsername != null)
            {
                ErrorMessage = "This username is already taken. Please choose another.";
                return Page();
            }

            // Hash the password using SHA256 (simple hashing - in production, use BCrypt or similar)
            var hashedPassword = HashPassword(Password);

            // Create new user
            var newUser = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username,
                Password = hashedPassword,
                Role = Role,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            _logger.LogInformation("New user registered: {Username} with role {Role}", Username, Role);

            // Redirect to login page with success message
            TempData["RegistrationSuccess"] = "Registration successful! Please log in with your credentials.";
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email {Email}", Email);
            ErrorMessage = "An error occurred during registration. Please try again.";
            return Page();
        }
    }

    private string HashPassword(string password)
    {
        // Simple SHA256 hashing - in production, use BCrypt.Net or similar
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}

