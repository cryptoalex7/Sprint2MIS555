using SkynetERP.Pages;

namespace SkynetERP.Services;

public class EmployeeService
{
    private readonly List<EmployeeModel> _employees = new()
    {
        new EmployeeModel 
        { 
            Id = 1,
            FirstName = "John", 
            LastName = "Smith", 
            Department = "IT", 
            Role = "Senior Developer", 
            Address = "123 Main St, City, State", 
            Phone = "(555)-123-4567", 
            Salary = 85000 
        },
        new EmployeeModel 
        { 
            Id = 2,
            FirstName = "Sarah", 
            LastName = "Johnson", 
            Department = "HR", 
            Role = "HR Manager", 
            Address = "456 Oak Ave, City, State", 
            Phone = "(555)-234-5678", 
            Salary = 75000 
        },
        new EmployeeModel 
        { 
            Id = 3,
            FirstName = "Mike", 
            LastName = "Davis", 
            Department = "Finance", 
            Role = "Financial Analyst", 
            Address = "789 Pine St, City, State", 
            Phone = "(555)-345-6789", 
            Salary = 65000 
        }
    };

    private int _nextId = 4;

    public List<EmployeeModel> GetAllEmployees()
    {
        return _employees.ToList();
    }

    public EmployeeModel? GetEmployeeById(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }

    public void AddEmployee(EmployeeModel employee)
    {
        employee.Id = _nextId++;
        _employees.Add(employee);
    }

    public bool DeleteEmployee(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            _employees.Remove(employee);
            return true;
        }
        return false;
    }

    public bool UpdateEmployee(EmployeeModel updatedEmployee)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
        if (employee != null)
        {
            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Department = updatedEmployee.Department;
            employee.Role = updatedEmployee.Role;
            employee.Address = updatedEmployee.Address;
            employee.Phone = updatedEmployee.Phone;
            employee.Salary = updatedEmployee.Salary;
            return true;
        }
        return false;
    }
}

