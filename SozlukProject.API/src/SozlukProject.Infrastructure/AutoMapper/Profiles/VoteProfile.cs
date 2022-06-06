using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Response;
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
            CreateMap<VoteCreateDto, Vote>();

            // When Updating
            CreateMap<VoteUpdateDto, Vote>();

            // When Listing/Getting
            CreateMap<Vote, VoteResponseDto>();
        }
    }
}
