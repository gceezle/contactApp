using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using contactApp.Contracts;
using contactApp.Data;

namespace contactApp.Services
{
    public class PersonReposiroty : BaseRepository<Person>, IPersonRepository
    {
        public PersonReposiroty(ContactAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
