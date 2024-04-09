using System;
using System.Collections.Generic;
using System.Linq;
using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Models;

namespace CRISP.BackendChallenge.Test.Data;

public static class MockEmployeeData
{
    public static IQueryable<Employee> GetAllEmployees()
    {
        return new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Test Employee 1",
                Department = Department.Engineering
            },
            new Employee
            {
                Id = 2,
                Name = "Test Employee 2",
                Department = Department.Management
            },
            new Employee
            {
                Id = 3,
                Name = "Test Employee 3",
                Department = Department.None
            }
        }.AsQueryable();
    }

    public static Employee Employee()
    {
        return new Employee
        {
            Name = "Test Employee",
            Department = Department.Engineering,
            StartDate = DateTime.Now,
            EndDate = null
        };
    }
    public static Employee EmployeeLoginDates()
    {
        return new Employee
        {
            Name = "Test Employee",
            Department = Department.Engineering,
            StartDate = DateTime.Now,
            Logins = new List<Login>
            {
                LoginDate(-1),
                LoginDate(-2)
            }
        };
    }
    
    public static IEnumerable<Employee> SearchEmployee()
    {
        return new List<Employee>
        {
            new Employee
            {
                Name = "Test Employee 2",
                Department = Department.Engineering,
                StartDate = DateTime.Now,
                Logins = new List<Login>
                {
                    LoginDate(-1),
                    LoginDate(-2)
                }
            }
        };

    }

    public static EmployeeRequest EmployeeRequest()
    {
        return new EmployeeRequest
        {
            Name = "Test Employee",
            Department = 1,
            StartDate = DateTime.Now,
            LoginDates = new List<DateTime>
            {
                DateTime.Now.AddMonths(-1),
                DateTime.Now.AddMonths(-2)
            }
        };
    }
    
    public static EmployeeRequest UpdateEmployeeRequest()
    {
        return new EmployeeRequest
        {
            Name = "Updated Name",
            Department = 0,
            StartDate = DateTime.Now,
            LoginDates = new List<DateTime>
            {
                DateTime.Now.AddMonths(-1)
            }
        };
    }
    
    public static Employee UpdatedEmployee()
    {
        return new Employee
        {
            Name = "Updated Name",
            Department = 0,
            StartDate = DateTime.Now,
            Logins = new List<Login>
            {
                LoginDate(-1)
            }
        };
    }
    

    public static EmployeeRequest BadEmployeeRequest_Name()
    {
        return new EmployeeRequest
        {
            Department = 1,
            StartDate = DateTime.Now,
        };
    }
    
    public static EmployeeRequest BadEmployeeRequest_Department()
    {
        return new EmployeeRequest
        {
            Name = "Test Employee",
            StartDate = DateTime.Now,
        };
    }

    public static Login LoginDate(int months)
    {
        return new Login
        {
            EmployeeId = 1,
            LoginDate = DateTime.Now.AddMonths(months)
        };
    }
}