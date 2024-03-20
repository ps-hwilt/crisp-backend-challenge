namespace CRISP.BackendChallenge.Context.Models;


/// <summary>
/// Person Entity Model
/// </summary>
public class Employee : BaseEntity
{
    public Employee()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Logins = new HashSet<Login>();
    }

    /// <summary>
    /// Name of the person
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The department of the person
    /// </summary>
    public Department Department { get; set; }

    /// <summary>
    /// The date which the employee was hired
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// The date in which the employee was terminated
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Navigation property to the logins of the person
    /// </summary>
    public virtual ICollection<Login> Logins { get; set; }
}