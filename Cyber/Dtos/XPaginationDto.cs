using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Dtos
{
    public class XPaginationDto
    { 
        public int total_record_count { get; set; }
        public int page_size { get; set; }
        public int page_no { get; set; }
        public int page_count { get; set; }
    }
}
