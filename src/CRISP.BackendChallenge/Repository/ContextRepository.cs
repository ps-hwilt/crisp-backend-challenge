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
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T Add(T entity)
    {
        _context.Add<T>(entity);
        _context.SaveChanges();

        return entity;
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    /// <inheritdoc />
    public T Update(T entity)
    {
       _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();

        return entity;
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges();
    }
}