using Cyber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions.Repositories
{
    public interface IAlertRepository : IRepository<Alert>
    {
        public Task<Alert> GetAlertByUniqueKey(string title, string description, int severity);

        public Task<Alert> GetAlertById(int id);
    }
}
