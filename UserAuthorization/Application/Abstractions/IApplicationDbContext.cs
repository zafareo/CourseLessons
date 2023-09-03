using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Role> Roles { get; }
        public DbSet<UserRole> UserRoles { get; }
        public DbSet<Permission> Permissions { get; }
        public DbSet<RolePermission> RolePermissions { get; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
