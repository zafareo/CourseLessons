using Application.Abstractions;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IApplicationDbContext _context;
        public PermissionRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Permission? permission = _context.Permissions.FirstOrDefault(x => x.PermissionId == id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<Permission>> GetAllAsync(Expression<Func<Permission, bool>>? expression)
        {
            var permissions = await _context.Permissions.ToListAsync();
            return permissions.AsQueryable(); ;
        }

        public async Task<Permission?> GetAsync(Expression<Func<Permission, bool>> expression)
        {
            Permission? permission = await _context.Permissions.FirstOrDefaultAsync(expression);
            return permission;
        }

        public async Task<bool> UpdateAsync(Permission entity)
        {
            Permission? permission = await _context.Permissions.FirstOrDefaultAsync(x => x.PermissionId == entity.PermissionId);
            if (permission != null)
            {
                _context.Permissions.Update(permission);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
