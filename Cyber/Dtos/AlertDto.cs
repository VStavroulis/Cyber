using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Dtos
{
    public class AlertDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int severity { get; set; }
        public List<string> ips { get; set; }
    }
}
