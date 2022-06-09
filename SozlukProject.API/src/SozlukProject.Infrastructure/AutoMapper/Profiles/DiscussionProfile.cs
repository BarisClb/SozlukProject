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
    public class DiscussionProfile : Profile
    {
        public DiscussionProfile()
        {
            // When Creating
            CreateMap<DiscussionCreateDto, Discussion>().ReverseMap();

            // When Updating
            CreateMap<DiscussionUpdateDto, Discussion>().ReverseMap();

            // When Listing/Reading
            CreateMap<Discussion, DiscussionReadDto>().ReverseMap();
        }
    }
}
