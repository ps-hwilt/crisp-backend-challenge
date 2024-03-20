using CRISP.BackendChallenge.Context;
using CRISP.BackendChallenge.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Repository;

public class ContextRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public ContextRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsQueryable();
    }

    /// <inheritdoc />
    public IEnumerable<Employee> GetAll()
    {
        var entities = _context.Employees.Include(e => e.Logins).ToList();
        if (entities == null)
        {
            throw new ArgumentNullException();
        }

        return entities;
    }

    /// <inheritdoc />
    public Employee GetById(int id)
    {
        var entity = _context.Employees
            .Include(e => e.Logins)
            .FirstOrDefault(e => e.Id == id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return entity;
    }

    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public void Add(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Add(entity);
        Save();
    }

    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Remove(entity);
        Save();
    }

    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public void Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Update(entity);
        Save();
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges();
    }
}