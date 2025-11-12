using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize(Roles = "Admin,HR")]
public class HRMModel : PageModel
{
    private readonly ILogger<HRMModel> _logger;
    private readonly UserService _userService;
    private readonly EmployeeService _employeeService;

    public HRMModel(ILogger<HRMModel> logger, UserService userService, EmployeeService employeeService)
    {
        _logger = logger;
        _userService = userService;
        _employeeService = employeeService;
    }

    public bool CanViewSalary { get; set; }
    public bool CanEditDelete { get; set; }
    public string? UserRole { get; set; }
    public string? Username { get; set; }
    public List<EmployeeModel> Employees { get; set; } = new();
    public int TotalEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public decimal AverageSalary { get; set; }
    public decimal MonthlyPayroll { get; set; }

    public void OnGet()
    {
        // Get user role from claims
        UserRole = User.FindFirst("Role")?.Value;
        Username = User.FindFirst(ClaimTypes.Name)?.Value;
        
        // Check if user can view salary
        CanViewSalary = _userService.CanViewSalary(UserRole);
        
        // Admin and HR can edit/delete employees (HR's core function)
        CanEditDelete = UserRole == "Admin" || UserRole == "HR";
        
        // Load employees
        Employees = _employeeService.GetAllEmployees();
        
        // Calculate statistics
        TotalEmployees = Employees.Count;
        TotalDepartments = Employees.Select(e => e.Department).Distinct().Count();
        
        if (Employees.Any() && CanViewSalary)
        {
            AverageSalary = Employees.Average(e => e.Salary);
            MonthlyPayroll = Employees.Sum(e => e.Salary) / 12;
        }
        else if (Employees.Any())
        {
            // If user can't view salary, show N/A or 0
            AverageSalary = 0;
            MonthlyPayroll = 0;
        }
        else
        {
            AverageSalary = 0;
            MonthlyPayroll = 0;
        }
    }

    public IActionResult OnGetExportEmployees()
    {
        var employees = _employeeService.GetAllEmployees();
        var userRole = User.FindFirst("Role")?.Value;
        var canViewSalary = _userService.CanViewSalary(userRole);
        
        // Generate CSV content
        var csv = new System.Text.StringBuilder();
        
        // Add header row
        csv.AppendLine("First Name,Last Name,Department,Role,Address,Phone" + (canViewSalary ? ",Salary" : ""));
        
        // Add data rows
        foreach (var employee in employees)
        {
            var salaryValue = canViewSalary ? employee.Salary.ToString() : "N/A";
            csv.AppendLine($"{EscapeCsvField(employee.FirstName)},{EscapeCsvField(employee.LastName)},{EscapeCsvField(employee.Department)},{EscapeCsvField(employee.Role)},{EscapeCsvField(employee.Address)},{EscapeCsvField(employee.Phone)}{(canViewSalary ? "," + salaryValue : "")}");
        }
        
        // Return CSV file
        var fileName = $"employees_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
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

    public IActionResult OnPostAddEmployee(EmployeeModel employee)
    {
        // Check authorization - Admin and HR can add employees
        var userRole = User.FindFirst("Role")?.Value;
        if (userRole != "Admin" && userRole != "HR")
        {
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Unauthorized: Only administrators and HR can add employees" });
        }

        if (!ModelState.IsValid)
        {
            Employees = _employeeService.GetAllEmployees();
            UserRole = User.FindFirst("Role")?.Value;
            Username = User.FindFirst(ClaimTypes.Name)?.Value;
            CanViewSalary = _userService.CanViewSalary(UserRole);
            CanEditDelete = UserRole == "Admin" || UserRole == "HR";
            return Page();
        }

        _employeeService.AddEmployee(employee);
        _logger.LogInformation("New employee added: {FirstName} {LastName}", employee.FirstName, employee.LastName);
        
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateEmployee(EmployeeModel employee)
    {
        // Check authorization - Admin and HR can edit employees
        var userRole = User.FindFirst("Role")?.Value;
        if (userRole != "Admin" && userRole != "HR")
        {
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Unauthorized: Only administrators and HR can edit employees" });
        }

        if (!ModelState.IsValid)
        {
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Invalid employee data" });
        }

        try
        {
            _logger.LogInformation("Update request received for employee ID: {EmployeeId}", employee.Id);
            
            var existingEmployee = _employeeService.GetEmployeeById(employee.Id);
            if (existingEmployee != null)
            {
                _employeeService.UpdateEmployee(employee);
                _logger.LogInformation("Employee updated: {EmployeeId} - {FullName}", employee.Id, employee.FullName);
                
                Response.ContentType = "application/json";
                return new JsonResult(new { success = true, message = "Employee updated successfully" });
            }
            
            _logger.LogWarning("Employee not found with ID: {EmployeeId}", employee.Id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Employee not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee with ID {EmployeeId}", employee.Id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }

    public IActionResult OnPostDeleteEmployee([FromForm] int id)
    {
        // Check authorization - Admin and HR can delete employees
        var userRole = User.FindFirst("Role")?.Value;
        if (userRole != "Admin" && userRole != "HR")
        {
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Unauthorized: Only administrators and HR can delete employees" });
        }

        try
        {
            _logger.LogInformation("Delete request received for employee ID: {EmployeeId}", id);
            
            var employee = _employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                _employeeService.DeleteEmployee(id);
                _logger.LogInformation("Employee deleted: {EmployeeId} - {FullName}", id, employee.FullName);
                
                Response.ContentType = "application/json";
                return new JsonResult(new { success = true, message = "Employee deleted successfully" });
            }
            
            _logger.LogWarning("Employee not found with ID: {EmployeeId}", id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "Employee not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee with ID {EmployeeId}", id);
            Response.ContentType = "application/json";
            return new JsonResult(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }
}

// Employee model for form binding
public class EmployeeModel
{
    public int Id { get; set; }

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
    [Range(0, 10000000, ErrorMessage = "Salary must be between 0 and 10,000,000")]
    public decimal Salary { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
