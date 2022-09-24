using Cyber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions.Repositories
{
    public interface IIPAddressRepository : IRepository<IPAddress>
    {
        public Task<IPAddress> GetIPAddressByIp(string ip);
    }
}
