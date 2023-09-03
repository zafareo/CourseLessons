using Domain.Entities;
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
        public DbSet<Project> Projects { get; }
        public DbSet<Qovuner> Qovuners { get; }
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
