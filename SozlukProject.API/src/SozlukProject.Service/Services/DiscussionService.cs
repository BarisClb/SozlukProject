using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Persistence.Repositories;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Response;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class DiscussionService : BaseService<Discussion, DiscussionCreateDto, DiscussionUpdateDto, DiscussionResponseDto>
    {
        private readonly IDiscussionRepository _discussionRepository;
        private readonly IMapper _mapper;

        public DiscussionService(IDiscussionRepository discussionRepository, IMapper mapper) : base(discussionRepository, mapper)
        {
            _discussionRepository = discussionRepository;
            _mapper = mapper;
        }


    }
}
