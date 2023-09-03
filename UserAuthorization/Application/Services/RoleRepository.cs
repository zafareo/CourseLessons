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
    public class RoleRepository : IRoleRepository
    {
        private readonly IApplicationDbContext _context;
        public RoleRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Role role)
        {
            _context.Roles.Add(role);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Role? role = _context.Roles.FirstOrDefault(x => x.Id == id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<Role>> GetAllAsync(Expression<Func<Role, bool>>? expression)
        {
            var roles = await _context.Roles.ToListAsync();
            return roles.AsQueryable();
        }

        public async Task<Role?> GetAsync(Expression<Func<Role, bool>> expression)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(expression);
            return role;
        }

        public async Task<bool> UpdateAsync(Role entity)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (role != null)
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
