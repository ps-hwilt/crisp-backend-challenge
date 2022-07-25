namespace CRISP.BackendChallenge.DTO;

/// <summary>
/// Response for Person API
/// </summary>
public class PersonResponse
{
    /// <summary>
    /// Id of the person
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the person
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Dates of the login for the person
    /// </summary>
    public List<DateTime>? LoginDates { get; set; }
}