using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace contactApp.Data
{
    [Table("tblPersons")]
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string first_name { get; set; }

        public string last_name { get; set; }

        [Required]
        public string phone { get; set; }




    }
}
