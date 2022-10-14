using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IRepository<Employee> _repository;

    public EmployeeController(ILogger<EmployeeController> logger, IRepository<Employee> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("~/GetAll")]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
        var result = _repository.Query()
            .ToList().Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                LoginDates = x.Logins.Select(x => x.LoginDate).ToList()
            });
        return Ok(result);
    }

    [HttpGet("~/GetById")]
    public IActionResult GetById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetById));

        var result = _repository.Query()
        .Where(x => x.Id == id)
        .Select(MapResult);

        return Ok(result);
    }

    [HttpPost("~/Create")]
    public IActionResult Create(Employee employee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Create));

        var result = MapResult(_repository.Add(employee));

        return Ok(result);
    }

    [HttpPut("~/Update")]
    public IActionResult Update(Employee employee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Update));
        
        var result = MapResult(_repository.Update(employee));

        return Ok(result);
    }

    [HttpDelete("~/Delete")]
    public IActionResult Delete(Employee employee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Delete));
        
        _repository.Delete(employee);

        return Ok();
    }

    [HttpGet("~/Search")]
    public IActionResult Search(int? id = null, string? name = null, int? department = null)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Search));

        var result = _repository.Query()
            .Where(x => (id.HasValue && x.Id == id)
            || (!string.IsNullOrEmpty(name) && x.Name == name)
            || (department.HasValue && x.Department == (Department)department))
            .Select(MapResult);

        return Ok(result);
    }
    

    private EmployeeResponse MapResult(Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            LoginDates = employee.Logins.OrderByDescending(l => l.LoginDate).Select(employee => employee.LoginDate).ToList()
        };
    }
}