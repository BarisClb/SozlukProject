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
    public class DiscussionProfile : Profile
    {
        public DiscussionProfile()
        {
            // When Creating
            CreateMap<DiscussionCreateDto, Discussion>();

            // When Updating
            CreateMap<DiscussionUpdateDto, Discussion>();

            // When Listing/Getting
            CreateMap<Discussion, DiscussionResponseDto>();
        }
    }
}
