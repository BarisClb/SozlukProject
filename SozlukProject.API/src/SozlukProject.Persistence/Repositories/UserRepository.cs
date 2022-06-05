using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SozlukProjectDbContext context) : base(context)
        { }
    }
}
