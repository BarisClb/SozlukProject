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
    public class UserActivationProfile : Profile
    {
        public UserActivationProfile()
        {
            // When Creating
            CreateMap<UserActivationCreateDto, UserActivation>().ReverseMap();

            // When Updating
            CreateMap<UserActivationUpdateDto, UserActivation>().ReverseMap();

            // When Listing/Reading
            CreateMap<UserActivation, UserActivationReadDto>().ReverseMap();
        }
    }
}
