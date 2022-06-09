using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.AutoMapper.Profiles
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            // When Creating
            CreateMap<VoteCreateDto, Vote>().ReverseMap();

            // When Updating
            CreateMap<VoteUpdateDto, Vote>().ReverseMap();

            // When Listing/Reading
            CreateMap<Vote, VoteReadDto>().ReverseMap();
        }
    }
}
