using Cyber.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _dbContext;

        public UnitOfWork(ApiDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        internal DbSet<T> GetDbSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
