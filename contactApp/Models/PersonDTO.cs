using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace contactApp.Models
{
    public class PersonDTO
    {
        [Required]
        public string first_name { get; set; }

        public string last_name { get; set; }

        [Required]
        public string phone { get; set; }
    }
}
