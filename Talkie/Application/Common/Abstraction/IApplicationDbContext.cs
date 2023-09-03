using Domain.Entities;
using Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstraction
{
    public interface IApplicationDbContext 
    {
        public DbSet<Permission> Permissions { get; }
        public DbSet<Role> Roles { get; }
        public DbSet<User> Users { get; }
        public DbSet<Post> Posts { get; }
        public DbSet<Comment> Comments { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
