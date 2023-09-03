using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class QovunerService : IQovunerRepository 
{
    private readonly IApplicationDbContext _context;
    public QovunerService(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Qovuner> CreateAsync(Qovuner entity)
    {
        _context.Set<Qovuner>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Qovuner? entity = _context.Set<Qovuner>().Find(id);
        if (entity == null)
        {
            _context.Set<Qovuner>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public Task<IQueryable<Qovuner>> GetAllAsync(Expression<Func<Qovuner, bool>> expression)
    {
        return Task.FromResult(_context.Set<Qovuner>().Where(expression));
    }

    public  Task<Qovuner?> GetAsync(Expression<Func<Qovuner, bool>> expression)
    {
        return Task.FromResult(_context.Set<Qovuner>().FirstOrDefault(expression));
    }

    public async Task<Qovuner?> UpdateAsync(Qovuner entity)
    {
        if (entity != null)
        {
            _context.Set<Qovuner>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        return null;
    }
}
