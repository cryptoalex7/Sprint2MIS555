using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SkynetERP.Services;

public class UserService
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    private readonly List<User> _users = new()
    {
        new User { Username = "admin", Password = "password", Role = "Admin", Email = "admin@skyneterp.com" },
        new User { Username = "hrmanager", Password = "hr123", Role = "HRManager", Email = "hr@skyneterp.com" },
        new User { Username = "staff", Password = "staff123", Role = "Staff", Email = "staff@skyneterp.com" }
    };

    public User? ValidateUser(string username, string password)
    {
        return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }

    public ClaimsPrincipal CreateClaimsPrincipal(User user)
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
        return role == "Admin" || role == "HRManager";
    }
}
