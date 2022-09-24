using Cyber.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
