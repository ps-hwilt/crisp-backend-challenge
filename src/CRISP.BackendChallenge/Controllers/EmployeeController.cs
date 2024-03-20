using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Login> _loginRepository;

    public EmployeeController(ILogger<EmployeeController> logger, IRepository<Employee> employeeRepository, IRepository<Login> loginRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
        _loginRepository = loginRepository;
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
            
        try
        {
            var employees = _employeeRepository.GetAll();

            var result = employees.Select(ToEmployeeResponse);

            return Ok(result);
        }
        catch (ArgumentNullException e)
        {
            _logger.LogDebug(":: There are no Employees in the Database: {EMessage}", e.Message);
            return NotFound(new {error = "No Employees Found in Database"});
        }
        catch (Exception e)
        {
            _logger.LogError(":: Error occurred while getting all employees: {EMessage}", e.Message);
            return CreateInternalServerError();
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName} with ID {Id}", nameof(GetById), id);
        try
        {
            var employee = _employeeRepository.GetById(id);
            return Ok(ToEmployeeResponse(employee));
        }
        catch (ArgumentNullException e)
        {
            _logger.LogDebug(":: Id Not Found: {Id}, {EMessage}", id, e.Message);
            return CreateNotFound(id);
        }
        catch (Exception e)
        {
            _logger.LogError(":: Error occurred while getting employee ID: {Id} {EMessage}", id, e.Message);
            return CreateInternalServerError();
        }
    }
    
    
    [HttpPost]
    [Route("createEmployee")]
    public IActionResult AddEmployee([FromBody] EmployeeRequest employeeRequest)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(AddEmployee));
        
        if (string.IsNullOrWhiteSpace(employeeRequest.Name))
        {
            _logger.LogError(":: Invalid EmployeeRequest provided");
            return CreateBadRequest("Creating an employee", "Name field required");
        }
        if (employeeRequest.Department == null)
        {
            _logger.LogError(":: Invalid EmployeeRequest provided");
            return CreateBadRequest("Creating an employee", "Department field required");
        }
        
        try
        {
            
            var employee = new Employee
            {
                Name = employeeRequest.Name,
                Department = (Department)employeeRequest.Department,
                StartDate = employeeRequest.StartDate,
                EndDate = employeeRequest.EndDate
            };

            _employeeRepository.Add(employee);
            _employeeRepository.Save();

            if (employeeRequest.LoginDates != null || employeeRequest.LoginDates!.Any())
            {
                foreach (var login in employeeRequest.LoginDates!.Select(l => new Login()
                         {
                             EmployeeId = employee.Id,
                             LoginDate = l
                         }))
                {
                    _loginRepository.Add(login);
                }
                _loginRepository.Save();
            }
            
            return Ok(ToEmployeeResponse(employee));
        }
        catch (Exception e)
        {
            _logger.LogError(":: Error occurred while creating employee: {EMessage}", e.Message);
            return CreateInternalServerError();
        }
    }

    
    [HttpGet]
    [Route("search")]
    public IActionResult SearchEmployees(int? id, string? name, int? department)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(SearchEmployees));

        try
        {
            if (id == null && string.IsNullOrWhiteSpace(name) && department == null)
            {
                var employees = _employeeRepository.GetAll();

                var res = employees.Select(ToEmployeeResponse);

                return Ok(res);
            }
            
            var result = _employeeRepository.Query().Include(e => e.Logins)
                .Where(x => (id.HasValue && x.Id == id) || (!string.IsNullOrEmpty(name) && x.Name == name) ||
                            (department.HasValue && x.Department == (Department)department))
                .Select(x => ToEmployeeResponse(x)).ToList();
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(":: Error occurred while searching employees: {EMessage}", e.Message);
            return CreateInternalServerError();
        }
        
    }
    
    
    [HttpPut]
    [Route("update/{id:int}")]
    public IActionResult UpdatedById(int id,[FromBody] EmployeeRequest employeeRequest)
    {
        _logger.LogDebug(":: Performing {MethodName} with ID {Id}", nameof(UpdatedById), id);
        
        try
        {
           
            var existingEmployee = _employeeRepository.GetById(id);
            
            existingEmployee.Name = employeeRequest.Name ?? existingEmployee.Name;
            if (employeeRequest.Department != null)
            {
                existingEmployee.Department = (Department)employeeRequest.Department;
            }
            existingEmployee.StartDate = employeeRequest.StartDate;
            existingEmployee.EndDate = employeeRequest.EndDate;
            
            _employeeRepository.Update(existingEmployee);
            _employeeRepository.Save();
            
            if (employeeRequest.LoginDates != null)
            {
                foreach (var login in employeeRequest.LoginDates.Select(l => new Login()
                         {
                             EmployeeId = existingEmployee.Id,
                             LoginDate = l
                         }))
                {
                    _loginRepository.Add(login);
                }
                _loginRepository.Save();
            }

            return Ok(ToEmployeeResponse(existingEmployee));
        }
        catch (ArgumentNullException e)
        {
            _logger.LogDebug(":: Employee Not Found: {Id}, {EMessage}", id, e.Message);
            return CreateNotFound(id);
        }
        catch (Exception e)
        {
            _logger.LogError(":: Error occurred while updating employee with ID {Id}: {EMessage}", id, e.Message);
            return CreateInternalServerError();
        }
    }
    

    [HttpDelete]
    [Route("delete/{id:int}")]
    public IActionResult DeleteById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName} with ID {Id}", nameof(DeleteById), id);
        try
        {
            var employee = _employeeRepository.GetById(id);
            
            
            var loginDates = _loginRepository.Query().Where(l => l.EmployeeId == id).ToList();
            foreach (Login login in loginDates)
            {
                _loginRepository.Delete(login);
                _loginRepository.Save();
            }

            _employeeRepository.Delete(employee);
            _employeeRepository.Save();

            return NoContent();
        }
        catch (ArgumentNullException e)
        {
            _logger.LogDebug(":: Employee Not Found: {Id}, {EMessage}", id, e.Message);
            return NotFound(new {error = "Employee Not Found", Id = id});
        }
        catch (Exception e)
        {
            _logger.LogError(":: Error occurred while deleting employee with ID {Id}: {EMessage}", id, e.Message);
            return CreateInternalServerError();
        }
    }
    

    private static EmployeeResponse ToEmployeeResponse(Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            LoginDates = employee.Logins.Where(login => login.EmployeeId == employee.Id)
                .Select(login => login.LoginDate)
                .ToList()
        };
    }

    private ObjectResult CreateInternalServerError()
    {
        return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request" });
    }

    private BadRequestObjectResult CreateBadRequest(string error, string reason)
    {
        return BadRequest(new { error, reason});
    }

    private NotFoundObjectResult CreateNotFound(int id)
    {
        return NotFound(new {error = "Employee Not Found", Id = id});
    }
    
}