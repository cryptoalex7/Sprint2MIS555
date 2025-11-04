using Microsoft.EntityFrameworkCore;
using SkynetERP.Data;
using SkynetERP.Models;
using SkynetERP.Pages;

namespace SkynetERP.Services;

public class EmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<EmployeeModel> GetAllEmployees()
    {
        return _context.Employees
            .Select(e => new EmployeeModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Department = e.Department,
                Role = e.Role,
                Address = e.Address,
                Phone = e.Phone,
                Salary = e.Salary
            })
            .ToList();
    }

    public EmployeeModel? GetEmployeeById(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null) return null;

        return new EmployeeModel
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Department = employee.Department,
            Role = employee.Role,
            Address = employee.Address,
            Phone = employee.Phone,
            Salary = employee.Salary
        };
    }

    public void AddEmployee(EmployeeModel employeeModel)
    {
        var employee = new Employee
        {
            FirstName = employeeModel.FirstName,
            LastName = employeeModel.LastName,
            Department = employeeModel.Department,
            Role = employeeModel.Role,
            Address = employeeModel.Address,
            Phone = employeeModel.Phone,
            Salary = employeeModel.Salary
        };

        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public bool DeleteEmployee(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool UpdateEmployee(EmployeeModel employeeModel)
    {
        var employee = _context.Employees.Find(employeeModel.Id);
        if (employee != null)
        {
            employee.FirstName = employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.Department = employeeModel.Department;
            employee.Role = employeeModel.Role;
            employee.Address = employeeModel.Address;
            employee.Phone = employeeModel.Phone;
            employee.Salary = employeeModel.Salary;

            _context.SaveChanges();
            return true;
        }
        return false;
    }
}

