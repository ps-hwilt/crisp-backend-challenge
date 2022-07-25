using CRISP.Backend.Challenge.Context;
using CRISP.Backend.Challenge.Context.Models;
using CRISP.BackendChallenge.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Context;

public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Person table accessor
    /// </summary>
    public DbSet<Employee> Employees { get; set; }

    /// <summary>
    /// Login table accessor
    /// </summary>
    public DbSet<Login> Logins { get; set; }

    /// <summary>
    /// Default Constructor
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=App.db");
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee {Id = 1, Name = "John Doe", Department = Department.Engineering },
            new Employee {Id = 2, Name = "Jane Doe", Department = Department.Management },
            new Employee {Id = 3, Name = "Joe Doe", Department = Department.Engineering }
        );
        modelBuilder.Entity<Login>().HasData(
            new Login {Id = 1, PersonId = 1, LoginDate = DateTime.Now.AddMonths(-1)},
            new Login {Id = 2, PersonId = 1, LoginDate = DateTime.Now.AddMonths(-2)},
            new Login {Id = 3, PersonId = 1, LoginDate = DateTime.Now.AddMonths(-3)},
            new Login {Id = 4, PersonId = 2, LoginDate = DateTime.Now.AddMonths(-1)},
            new Login {Id = 5, PersonId = 2, LoginDate = DateTime.Now.AddMonths(-2)},
            new Login {Id = 6, PersonId = 3, LoginDate = DateTime.Now.AddMonths(-1)});
    }
}