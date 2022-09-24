using Cyber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions.Repositories
{
    public interface IAlertIpsRepository : IRepository<AlertIP>
    {
        public Task<AlertIP> GetAlertIPByKey(int alertId, int ipAddressId);
    }
}
