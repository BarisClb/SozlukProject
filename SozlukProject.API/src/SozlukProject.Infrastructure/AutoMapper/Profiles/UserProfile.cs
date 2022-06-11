using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Service.Dtos.Account;
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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // When Creating
            CreateMap<UserCreateDto, User>().ReverseMap();

            // When Updating
            CreateMap<UserUpdateDto, User>().ReverseMap();

            // When Listing/Reading
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, UserDiscussionPageReadDto>().ReverseMap();

            // For Logging in after Registering
            CreateMap<UserReadDto, AccountLoginInfoDto>()
                .ForMember(destination => destination.Account, option => option.MapFrom(source => source.Email));
        }
    }
}
