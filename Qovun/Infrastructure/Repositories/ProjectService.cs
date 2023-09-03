using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ProjectService : IProjectRepository
{
    private readonly IApplicationDbContext _context;
    public ProjectService(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Project> CreateAsync(Project entity)
    {
        _context.Set<Project>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = _context.Set<Project>().Find(id);
        if (entity == null)
        {
            _context.Set<Project>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public Task<IQueryable<Project>> GetAllAsync(Expression<Func<Project, bool>> expression)
    {
        return Task.FromResult(_context.Set<Project>().Where(expression));
    }

    public Task<Project?> GetAsync(Expression<Func<Project, bool>> expression)
    {
        return Task.FromResult(_context.Set<Project>().FirstOrDefault(expression));
    }

    public async Task<Project?> UpdateAsync(Project entity)
    {
        if (entity != null)
        {
            _context.Set<Project>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        return null;
    }
}
