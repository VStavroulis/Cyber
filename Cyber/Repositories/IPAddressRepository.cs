using Cyber.Abstractions;
using Cyber.Abstractions.Repositories;
using Cyber.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Repositories
{
    public class IPAddressRepository : Repository<IPAddress>, IIPAddressRepository
    {
        public IPAddressRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IPAddress> GetIPAddressByIp(string ip)
        {
            var iPAddresses = await GetAll();
            return iPAddresses.Where(x => x.IP == ip).FirstOrDefault();
        }

      
    }
}
