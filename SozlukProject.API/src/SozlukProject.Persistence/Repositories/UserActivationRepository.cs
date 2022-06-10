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
    public class UserActivationRepository : GenericRepository<UserActivation>, IUserActivationRepository
    {
        public UserActivationRepository(SozlukProjectDbContext context) : base(context)
        { }
    }
}
