using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task SaveChangesAsync();
    }
}
