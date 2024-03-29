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
    public IEnumerable<T> GetAll()
    {
        var entities = _context.Employees.Include(e => e.Logins).ToList();
        if (entities == null!)
        {
            return null!;
        }

        return (entities as IEnumerable<T>)!;
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        Employee? entity = _context.Employees
            .Include(e => e.Logins)
            .FirstOrDefault(e => e.Id == id);
        if (entity == null)
        {
            return null!;
        }
        return (entity as T)!;
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
    public T Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        var existingEntity = _context.Employees
            .Include(e => e.Logins)
            .FirstOrDefault(x => x.Id == entity.Id);

        if (existingEntity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {entity.Id} not found.");
        }
        
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        UpdateLogins(existingEntity, entity);
        
        Save();

        return (existingEntity as T)!;
    }

    private void UpdateLogins(Employee existingEntity, T updatedEntity)
    {
        if (updatedEntity is Employee updatedEmployeeEntity)
        {
            if (updatedEmployeeEntity.Logins != null!)
            {
                foreach (var login in updatedEmployeeEntity.Logins)
                {
                    existingEntity.Logins.Add(login);
                }
            }
        }
        
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges();
    }
}