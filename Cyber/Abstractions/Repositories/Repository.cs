using Cyber.Abstractions.Entities;
using Cyber.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private DbSet<T> _objectSet;

        public Repository(IUnitOfWork unitOfWork)
        {
            var efunitOfWork = unitOfWork as UnitOfWork;
            _objectSet = efunitOfWork.GetDbSet<T>();
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
          
            return await _objectSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _objectSet.FirstOrDefaultAsync();
        }

        public void Insert(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Update(T entity)
        {
            _objectSet.Update(entity);
        }

    }
}
