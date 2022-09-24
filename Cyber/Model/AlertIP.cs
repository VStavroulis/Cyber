using Cyber.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Model
{
    public class AlertIP : EntityBase
    {
        public int AlertId { get; set; }
        public Alert Alert { get; set; }

        public int IPAddressId { get; set; }
        public IPAddress IPAddress { get; set; }
    }
}
