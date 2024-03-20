using CRISP.BackendChallenge.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Context;

public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Person table accessor
    /// </summary>
    public DbSet<Employee> Employees { get; set; } = null!;

    /// <summary>
    /// Login table accessor
    /// </summary>
    public DbSet<Login> Logins { get; set; } = null!;

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
            new Employee
            {
                Id = 1, Name = "John Doe", Department = Department.Engineering,
                StartDate = DateTime.Parse("01/01/2019"), EndDate = null,
            },
            new Employee
            {
                Id = 2, Name = "Jane Doe", Department = Department.Management, StartDate = DateTime.Parse("01/01/2016"),
            },
            new Employee
            {
                Id = 3, Name = "Joe Doe", Department = Department.Engineering, StartDate = DateTime.Parse("01/01/2022"),
            },
            new Employee
            {
                Id = 4, Name = "Leroy Jenkins", Department = Department.Engineering,
                StartDate = DateTime.Parse("01/01/2006"), EndDate = DateTime.Parse("01/01/2019")
            }
        );

        modelBuilder.Entity<Login>().HasData(
            new Login
            {
                Id = 1, EmployeeId = 1, LoginDate = DateTime.Now.AddMonths(-1)
            },
            new Login
            {
                Id = 2, EmployeeId = 1, LoginDate = DateTime.Now.AddMonths(-2)
            },
            new Login
            {
                Id = 3, EmployeeId = 1, LoginDate = DateTime.Now.AddMonths(-3)
            },
            new Login
            {
                Id = 4, EmployeeId = 2, LoginDate = DateTime.Now.AddMonths(-1)
            },
            new Login
            {
                Id = 5, EmployeeId = 2, LoginDate = DateTime.Now.AddMonths(-2)
            },
            new Login
            {
                Id = 6, EmployeeId = 3, LoginDate = DateTime.Now.AddMonths(-1)
            });
    }
}