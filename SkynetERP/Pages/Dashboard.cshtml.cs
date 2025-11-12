using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using SkynetERP.Data;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly ILogger<DashboardModel> _logger;
    private readonly UserService _userService;
    private readonly EmployeeService _employeeService;
    private readonly VendorService _vendorService;
    private readonly DeliveryService _deliveryService;
    private readonly ApplicationDbContext _context;

    public DashboardModel(
        ILogger<DashboardModel> logger, 
        UserService userService,
        EmployeeService employeeService,
        VendorService vendorService,
        DeliveryService deliveryService,
        ApplicationDbContext context)
    {
        _logger = logger;
        _userService = userService;
        _employeeService = employeeService;
        _vendorService = vendorService;
        _deliveryService = deliveryService;
        _context = context;
    }

    public string? UserRole { get; set; }
    public string? Username { get; set; }
    public bool CanAccessHRM { get; set; }
    public bool CanAccessVendor { get; set; }
    public int TotalEmployees { get; set; }
    public int TotalVendors { get; set; }
    public int TotalDeliveries { get; set; }
    public int TotalUsers { get; set; }

    public void OnGet()
    {
        // Get user role from claims
        UserRole = User.FindFirst("Role")?.Value ?? User.FindFirst(ClaimTypes.Role)?.Value;
        Username = User.FindFirst(ClaimTypes.Name)?.Value;

        // Determine access based on role
        CanAccessHRM = UserRole == "Admin" || UserRole == "HR";
        CanAccessVendor = UserRole == "Admin" || UserRole == "Vendor";

        // Load statistics for Admin users
        if (UserRole == "Admin")
        {
            var employees = _employeeService.GetAllEmployees();
            var vendors = _vendorService.GetAllVendors();
            var deliveries = _deliveryService.GetAllDeliveries();
            
            TotalEmployees = employees.Count;
            TotalVendors = vendors.Count;
            TotalDeliveries = deliveries.Count;
            
            // Get total users from database
            try
            {
                TotalUsers = _context.Users.Count();
            }
            catch
            {
                TotalUsers = 0;
            }
        }
    }
}

