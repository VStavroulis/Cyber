using Cyber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions.Services
{
    public interface IAlertIpsService
    {
        public Task<AlertIP> GetAlertIps();
    }
}
