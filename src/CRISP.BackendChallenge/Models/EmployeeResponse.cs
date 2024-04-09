namespace CRISP.BackendChallenge.Models;

/// <summary>
/// Response for Employee API
/// </summary>
public class EmployeeResponse
{
    /// <summary>
    /// Id of the person
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name of the person
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Dates of the login for the person
    /// </summary>
    public List<DateTime>? LoginDates { get; init; }
}