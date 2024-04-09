using System.ComponentModel.DataAnnotations;

namespace CRISP.BackendChallenge.Models;

public class EmployeeRequest
{
    /// <summary>
    /// Name of the person
    /// </summary>
    [Required]
    public string Name { get; init; }

    /// <summary>
    /// The department of the person
    /// </summary>
    [Required]
    public int Department { get; init; }

    /// <summary>
    /// The date which the employee was hired
    /// </summary>
    public DateTime? StartDate { get; init; }
    
    /// <summary>
    /// The date which the employee was fired
    /// </summary>
    public DateTime? EndDate { get; init; }
    
    /// <summary>
    /// List of the employee's login dates
    /// </summary>
    public List<DateTime>? LoginDates { get; init; }
    
}