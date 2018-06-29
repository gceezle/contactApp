using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using contactApp.Models;

namespace contactApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(ContactAppDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Persons.Any())
            {
                return;
            }

            var filler = new Filler<PersonDTO>();
            filler.Setup()
                .OnProperty(x => x.first_name).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(x => x.last_name).Use(new RealNames(NameStyle.LastName))
                .OnProperty(x => x.phone).Use(new PatternGenerator("+{N:11}"));


            IEnumerable<PersonDTO> _persons = filler.Create(20);

            var persons = Mapper.Map<List<Person>>(_persons);

            context.Persons.AddRange(persons);
            context.SaveChanges();
        }
    }
}
