using Cyber.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Model
{
    public class Alert : EntityBase
    {

        [Required(ErrorMessage = "Title cannot be null or empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description cannot be null or empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Severity cannot be null or empty")]
        public int Severity { get; set; }

        //Navigation Properties
        public List<AlertIP> AlertIPs { get; set; }
    }
}
