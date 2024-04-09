using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CRISP.BackendChallenge.Controllers;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static CRISP.BackendChallenge.Test.Data.MockEmployeeData;
using Employee = CRISP.BackendChallenge.Context.Models.Employee;

namespace CRISP.BackendChallenge.Test.Controllers;

public class TestEmployeeController
{
    private readonly Mock<IRepository<Employee>> _employeeRepository = new();
    private readonly Mock<ILogger<EmployeeController>> _logger = new();

    private EmployeeController CreateEmployeeController()
    {
        return new EmployeeController(_logger.Object, _employeeRepository.Object);
    }
    
    [Fact]
    public void GetAll_ReturnsOkResult()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.GetAll()).Returns(GetAllEmployees);
        
        // Act
        var result = controller.GetAll();
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var employees = Assert.IsAssignableFrom<List<EmployeeResponse>>(okResult.Value);
        Assert.Equal(3, employees.Count());
    }

    [Fact]
    public void GetAll_ReturnsNotFoundError()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.GetAll()).Returns((IEnumerable<Employee>)null!);
        
        // Act
        var result = controller.GetAll();
        
        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal("{ error = No Employees Found in Database }", notFoundResult.Value!.ToString());
    }
    
    [Fact]
    public void GetById_ReturnsOkResult()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.GetById(1)).Returns(EmployeeLoginDates());
        
        // Act
        var result = controller.GetById(1);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var employee = Assert.IsAssignableFrom<EmployeeResponse>(okResult.Value);
        Assert.Equal("Test Employee", employee.Name);
        Debug.Assert(employee.LoginDates != null, "employee.LoginDates != null");
        Assert.Equal(2, employee.LoginDates.Count);
        
    }
    
    [Fact]
    public void GetById_ReturnsNotFoundResult()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.GetById(0)).Returns((Employee)null!);
        
        // Act
        var result = controller.GetById(0);
        
        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal("{ error = Employee Not Found, Id = 0 }", notFoundResult.Value!.ToString()!);
    }
    
    [Fact]
    public void AddEmployee_ReturnsOkResult()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Add(EmployeeLoginDates()));
        
        // Act
        var result = controller.AddEmployee(EmployeeRequest());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var employeeResponse = Assert.IsType<EmployeeResponse>(okResult.Value);
        Assert.Equal("Test Employee", employeeResponse.Name);
        Assert.Equal(0, employeeResponse.Id);
        Debug.Assert(employeeResponse.LoginDates != null, "employeeResponse.LoginDates != null");
        Assert.Equal(2, employeeResponse.LoginDates.Count);
    }

    [Fact]
    public void DeleteEmployee_ReturnsNoContent()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Delete(1)).Returns(true);
        
        // Act
        var result = controller.DeleteById(1);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public void DeleteEmployee_ReturnsNotFound()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Delete(10)).Returns(false);
        
        // Act
        var result = controller.DeleteById(10);
        
        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public void SearchEmployees_WithName_ReturnsOkResult()
    {
        // Arrange
        const string name = "Test Employee 2";
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Search(name, null)).Returns(SearchEmployee);

        // Act
        var result = controller.SearchEmployees(name, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var employees = Assert.IsAssignableFrom<IEnumerable<EmployeeResponse>>(okResult.Value);
        Assert.Single(employees);
    }

    [Fact]
    public void SearchEmployees_WithDepartment_ReturnsOkResult()
    {
        // Arrange
        const int department = 1;
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Search(null, department)).Returns(SearchEmployee);

        // Act
        var result = controller.SearchEmployees(null, department);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var employees = Assert.IsAssignableFrom<IEnumerable<EmployeeResponse>>(okResult.Value);
        Assert.Single(employees);
    }

    [Fact]
    public void SearchEmployees_WithNoParameters_ReturnsOkResult()
    {
        // Arrange
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Search(null, null)).Returns(GetAllEmployees());

        // Act
        var result = controller.SearchEmployees(null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var employees = Assert.IsAssignableFrom<IEnumerable<EmployeeResponse>>(okResult.Value);
        Assert.Equal(3, employees.Count());
    }
    
    [Fact]
    public void UpdateEmployee_ReturnsOkResult()
    {
        // Arrange
        const int id = 0;
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Update(It.IsAny<Employee>())).Returns(UpdatedEmployee());

        // Act
        var result = controller.UpdatedById(id, UpdateEmployeeRequest());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var employeeResponse = Assert.IsType<EmployeeResponse>(okResult.Value);
        Assert.Equal("Updated Name", employeeResponse.Name);
        Assert.Equal(0, employeeResponse.Id);
        Debug.Assert(employeeResponse.LoginDates != null, "employeeResponse.LoginDates != null");
        Assert.Single(employeeResponse.LoginDates);
    }
    
    [Fact]
    public void UpdateEmployee_ReturnsNotFoundResult()
    {
        // Arrange
        const int id = 0;
        var controller = CreateEmployeeController();
        _employeeRepository.Setup(x => x.Update(It.IsAny<Employee>())).Returns((Employee?)null);
        
        // Act
        var result = controller.UpdatedById(id, UpdateEmployeeRequest());
        
        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal("{ error = Employee Not Found, Id = 0 }", notFoundResult.Value!.ToString()!);
    }

}
