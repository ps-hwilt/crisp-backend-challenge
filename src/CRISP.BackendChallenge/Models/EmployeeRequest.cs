namespace CRISP.BackendChallenge.Models;

public class EmployeeRequest
{
    /// <summary>
    /// Name of the person
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The department of the person
    /// </summary>
    public int? Department { get; set; }

    /// <summary>
    /// The date which the employee was hired
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// The date which the employee was fired
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// List of the employee's login dates
    /// </summary>
    public List<DateTime>? LoginDates { get; set; }
    
}