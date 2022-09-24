using Cyber.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Abstractions.Services
{
    public interface IIPAdressService
    {
        public Task<IPAddressDto> GetIpInfo(string ipAddress);
    }
}
