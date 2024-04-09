using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Filters;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using CRISP.BackendChallenge.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CRISP.BackendChallenge.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ExceptionFilter))]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IRepository<Employee> _employeeRepository;

    public EmployeeController(ILogger<EmployeeController> logger, IRepository<Employee> employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));

        var employees = _employeeRepository.GetAll();

        if (employees == null) return NotFound(new { error = ErrorConstants.EmployeesNotFound });

        var result = employees.Select(x => new EmployeeResponse
        {
            Id = x.Id,
            Name = x.Name,
            LoginDates = x.Logins.Select(l => l.LoginDate).ToList()
        }).ToList();

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName} with ID {Id}", nameof(GetById), id);

        var employee = _employeeRepository.GetById(id);
        
        if (employee == null) return CreateNotFound(id);
        
        return Ok(new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            LoginDates = employee.Logins.Select(l => l.LoginDate).ToList()
        });
    }
    
    
    [HttpPost]
    [Route("createEmployee")]
    [TypeFilter(typeof(EmployeeModelStateFilter))]
    public IActionResult AddEmployee([FromBody] EmployeeRequest employeeRequest)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(AddEmployee));

        var logins = employeeRequest.LoginDates?.Select(loginDate => new Login {
            LoginDate = loginDate
        }).ToList() ?? new List<Login>();
        
        var employee = new Employee
        {
            Name = employeeRequest.Name,
            Department = (Department)employeeRequest.Department,
            StartDate = employeeRequest.StartDate,
            EndDate = employeeRequest.EndDate,
            Logins = logins
        };

        _employeeRepository.Add(employee);
        
        return Ok(new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            LoginDates = employee.Logins.Select(l => l.LoginDate).ToList()
        });
    }

    
    [HttpGet]
    [Route("search")]
    public IActionResult SearchEmployees(string? name, int? department)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(SearchEmployees));
        
        var employees = _employeeRepository.Search(name, department);

        if (employees == null) return NotFound(new { error = ErrorConstants.EmployeesNotFound });
        
        var result = employees.Select(x => new EmployeeResponse
        {
            Id = x.Id,
            Name = x.Name,
            LoginDates = x.Logins.Select(l => l.LoginDate).ToList()
        }).ToList();
        
        return Ok(result);
    
        
    }
    
    
    [HttpPut]
    [Route("update/{id:int}")]
    [TypeFilter(typeof(EmployeeModelStateFilter))]
    public IActionResult UpdatedById(int id,[FromBody] EmployeeRequest employeeRequest) 
    {
        _logger.LogDebug(":: Performing {MethodName} with ID {Id}", nameof(UpdatedById), id);

        var logins = employeeRequest.LoginDates?.Select(loginDate => new Login {
            EmployeeId = id,
            LoginDate = loginDate
        }).ToList() ?? new List<Login>();
        
        var employee = new Employee
        {
            Id = id,
            Name = employeeRequest.Name,
            Department = (Department)employeeRequest.Department,
            StartDate = employeeRequest.StartDate,
            EndDate = employeeRequest.EndDate,
            Logins = logins
        };
        
        var updatedEmployee = _employeeRepository.Update(employee);

        if (updatedEmployee == null) return CreateNotFound(id);
        
        return Ok(new EmployeeResponse
        {
            Id = updatedEmployee.Id,
            Name = updatedEmployee.Name,
            LoginDates = updatedEmployee.Logins.Select(l => l.LoginDate).ToList()
        });
    }
    

    [HttpDelete]
    [Route("delete/{id:int}")]
    public IActionResult DeleteById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName} with ID {Id}", nameof(DeleteById), id);
        
        var isDeleted = _employeeRepository.Delete(id);
        
        if (isDeleted) return NoContent();
        
        return CreateNotFound(id);
    }
    
    
    private NotFoundObjectResult CreateNotFound(int id)
    {
        return NotFound(new {error = ErrorConstants.EmployeeNotFound, Id = id});
    }
    
}