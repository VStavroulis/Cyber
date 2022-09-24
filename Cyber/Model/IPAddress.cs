using Cyber.Abstractions.Entities;
using Cyber.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Model
{
    public class IPAddress : EntityBase
    {

        [Required(ErrorMessage = "IP cannot be null or empty")]
        public string IP { get; set; }

        [Required(ErrorMessage = "BlackListed cannot be null or empty")]
        public bool BlackListed { get; set; }

        [Required(ErrorMessage = "SourceType cannot be null or empty")]
        public SourceType SourceType { get; set; }

        public int IPCounter {get;set;}

        //Navigation Properties
        public List<AlertIP> AlertIPs { get; set; }
    }
}
