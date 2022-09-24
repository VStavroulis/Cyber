using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Dtos
{
    public class IPAddressDto
    {
        public bool blacklisted { get; set; }
        public int sourceType { get; set; }
    }
}
