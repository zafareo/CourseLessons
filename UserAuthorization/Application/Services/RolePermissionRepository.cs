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
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly IApplicationDbContext _context;
        public RolePermissionRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(RolePermission rolePermission)
        {
            _context.RolePermissions.Add(rolePermission);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            RolePermission? rp = await _context.RolePermissions.FirstOrDefaultAsync(x => x.RolePermissionId == id);
            if (rp != null)
            {
                _context.RolePermissions.Remove(rp);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<RolePermission>> GetAllAsync(Expression<Func<RolePermission, bool>>? expression)
        {
            var rp = await _context.RolePermissions.ToListAsync();
            return rp.AsQueryable();
        }

        public async Task<RolePermission?> GetAsync(Expression<Func<RolePermission, bool>> expression)
        {
            RolePermission? rp = await _context.RolePermissions.FirstOrDefaultAsync(expression);
            return rp;
        }

        public async Task<bool> UpdateAsync(RolePermission entity)
        {
            RolePermission? rp = await _context.RolePermissions.FirstOrDefaultAsync(x => x.RolePermissionId == entity.RolePermissionId);
            if (rp != null)
            {
                _context.RolePermissions.Update(rp);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
