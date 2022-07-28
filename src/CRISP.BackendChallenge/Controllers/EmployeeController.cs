using CRISP.Backend.Challenge.Context;
using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.DTO;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRISP.BackendChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IRepository _repository;

    public EmployeeController(ILogger<EmployeeController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
        var result = _repository.Query<Employee>()
            .ToList().Select(x => new PersonResponse
            {
                Id = x.Id,
                Name = x.Name,
                // TODO: Include the login date information...
                LoginDates = null
            });
        return Ok(result);
    }
    // TODO: Implement Search By Id
    // TODO: Implement Delete by Id
    // TODO: Implement Update by Id
}