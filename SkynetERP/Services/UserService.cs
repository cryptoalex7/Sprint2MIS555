using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SkynetERP.Data;
using SkynetERP.Models;

namespace SkynetERP.Services;

public class UserService
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserDto? ValidateUser(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        
        if (user == null)
        {
            // Fallback to hardcoded users for backward compatibility
            return ValidateHardcodedUser(email, password);
        }

        // Hash the provided password and compare
        var hashedPassword = HashPassword(password);
        if (user.Password == hashedPassword)
        {
            return new UserDto
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role,
                Email = user.Email
            };
        }

        return null;
    }

    private UserDto? ValidateHardcodedUser(string email, string password)
    {
        var hardcodedUsers = new List<UserDto>
        {
            new UserDto { Username = "admin", Password = "password", Role = "Admin", Email = "admin@erp.com" },
            new UserDto { Username = "hr", Password = "hr123", Role = "HR", Email = "hr@erp.com" },
            new UserDto { Username = "vendor", Password = "vendor123", Role = "Vendor", Email = "vendor@erp.com" },
            new UserDto { Username = "user", Password = "user123", Role = "User", Email = "user@erp.com" }
        };

        return hardcodedUsers.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    private string HashPassword(string password)
    {
        // Simple SHA256 hashing - matches the registration hashing
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public ClaimsPrincipal CreateClaimsPrincipal(UserDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("Role", user.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    public bool CanViewSalary(string? role)
    {
        return role == "Admin" || role == "HR";
    }

    public bool CanViewSpend(string? role)
    {
        // Only Admin can view annual spend (sensitive financial information)
        // Similar to salary restrictions - financial data should be restricted
        return role == "Admin";
    }
}
