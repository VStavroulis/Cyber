using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Dtos
{
    public class StatisticsDto
    {
        public int blackListed { get; set; }
        public int internalIPs { get; set; }
        public int externalIPs { get; set; }
    }
}
