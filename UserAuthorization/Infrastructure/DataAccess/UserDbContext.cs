using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class UserDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set;}
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }
    }
}
