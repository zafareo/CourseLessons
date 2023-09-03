using Application.Abstractions;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;
        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<string> ComputeHashAsync(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder builder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return Task.FromResult(builder.ToString());
        }


        public async Task<bool> CreateAsync(User user)
        {
            //var roles = new List<UserRole>();
            //foreach(var role in user.Roles)
            //{
            //    roles.Add(new UserRole()
            //    {
            //        Role = await _context.Roles.FindAsync(role)
            //    });
            //}
            //user.UserRoles = roles;
            user.Password = await ComputeHashAsync(user.Password);
            await _context.Users.AddAsync(user);
            int res =  await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            User? user = _context.Users.FirstOrDefault(x => x.UsersId == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>>? expression )
        {
           return expression == null ? await Task.FromResult(_context.Users.AsQueryable()) : 
                 await Task.FromResult(_context.Users.Where(expression));
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> expression)
        {
            User? user = await _context.Users.Where(expression)
                .Include(x=>x.UserRoles)
                .ThenInclude(x=>x.Role)
                .ThenInclude(x=>x.RolePermissions)
                .ThenInclude(x=>x.Permission)
                .Select(x=>x).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(User entity)
        {          
                _context.Users.Update(entity);
                int res = await _context.SaveChangesAsync();
                return res > 0;
        }
    }
}
