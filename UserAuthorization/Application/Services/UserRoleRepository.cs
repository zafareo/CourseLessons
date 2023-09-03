using Application.Abstractions;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IApplicationDbContext _context;
        public UserRoleRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            UserRole? userRole = _context.UserRoles.FirstOrDefault(x => x.UserRoleId == id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<UserRole>> GetAllAsync(Expression<Func<UserRole, bool>>? expression)
        {
            var userRoles = await _context.UserRoles.ToListAsync();
            return userRoles.AsQueryable();
        }

        public async Task<UserRole?> GetAsync(Expression<Func<UserRole, bool>> expressionn)
        {
            UserRole? userRole = await _context.UserRoles.FirstOrDefaultAsync(expressionn);
            return userRole;
        }

        public async Task<bool> UpdateAsync(UserRole entity)
        {
            UserRole? userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserRoleId == entity.UserRoleId);
            if (userRole != null)
            {
                _context.UserRoles.Update(userRole);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
