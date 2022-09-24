using Cyber.Abstractions;
using Cyber.Abstractions.Repositories;
using Cyber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Repositories
{
    public class AlertRepository : Repository<Alert>, IAlertRepository
    {
        public AlertRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Alert> GetAlertById(int id)
        {
            var alerts = await GetAll();
            return alerts.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task<Alert> GetAlertByUniqueKey(string title, string description, int severity)
        {
            var alerts = await GetAll();
            return alerts.Where(x => x.Title == title && x.Description == description && x.Severity == severity).FirstOrDefault();
        }
    }
}
