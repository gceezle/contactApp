using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace contactApp.Data
{
    public class ContactAppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public ContactAppDbContext(DbContextOptions<ContactAppDbContext> options) : base(options)
        {
        }

       
    }
}
