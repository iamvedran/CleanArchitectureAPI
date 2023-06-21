using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DatabaseContext.LeaveDatabaseContext _context;

    public GenericRepository(DatabaseContext.LeaveDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
        
    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}