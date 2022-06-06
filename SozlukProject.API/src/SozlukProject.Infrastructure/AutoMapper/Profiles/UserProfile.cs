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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // When Creating
            CreateMap<UserCreateDto, User>();

            // When Updating
            CreateMap<UserUpdateDto, User>();

            // When Listing/Getting
            CreateMap<User, UserResponseDto>();
        }
    }
}
