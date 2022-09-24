using Cyber.Abstractions;
using Cyber.Abstractions.Repositories;
using Cyber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Repositories
{
    public class AlertIpsRepository : Repository<AlertIP>, IAlertIpsRepository
    {
        public AlertIpsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<AlertIP> GetAlertIPByKey(int alertId, int ipAddressId)
        {
            var alerts = await GetAll();
            return alerts.Where(x => x.AlertId == alertId && x.IPAddressId == ipAddressId).FirstOrDefault();
        }
    }
}
