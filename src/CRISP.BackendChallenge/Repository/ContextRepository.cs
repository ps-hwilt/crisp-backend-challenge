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

    public IQueryable<Employee> Query()
    {
        return _context.Set<Employee>().AsQueryable();
    }

    /// <inheritdoc />
    public IEnumerable<Employee> GetAll()
    {
        var entities = _context.Employees.Include(e => e.Logins).ToList();
        return entities;
    }

    /// <inheritdoc />
    public Employee? GetById(int id)
    {
        Employee? entity = _context.Employees
            .Include(e => e.Logins)
            .FirstOrDefault(e => e.Id == id);
        return entity ?? null;
    }

    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public void Add(Employee entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Add(entity);
        Save();
    }
    
    public bool Delete(int id)
    {
        Employee? entity = _context.Employees
            .Include(e => e.Logins)
            .FirstOrDefault(e => e.Id == id);
        
        if (entity == null) return false;
        
        _context.Remove(entity);
        Save();
        
        return true;
    }
    
    public Employee? Update(Employee entity)
    {
        var existingEntity = _context.Employees
            .Include(e => e.Logins)
            .FirstOrDefault(x => x.Id == entity.Id);

        if (existingEntity == null) return null;
        
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        foreach (var login in entity.Logins)
        {
            existingEntity.Logins.Add(login);
        }
        
        Save();

        return existingEntity;
    }
    
    public IEnumerable<Employee> Search(string? name, int? department)
    {
        var query = _context.Set<Employee>().AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(x => x.Name == name);

        if (department.HasValue)
            query = query.Where(x => x.Department == (Department)department);
        
        query = query.Include(e => e.Logins);
        return query;
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges();
    }
}