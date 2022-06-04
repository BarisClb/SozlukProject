using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class DiscussionService : BaseService<Discussion>
    {
        private readonly IDiscussionRepository _discussionRepository;

        public DiscussionService(IDiscussionRepository discussionRepository) : base (discussionRepository)
        {
            _discussionRepository = discussionRepository;
        }



    }
}
