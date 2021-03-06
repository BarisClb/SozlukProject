using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Service.Dtos.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.AutoMapper.Profiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<BaseEntity, BaseEntityReadDto>();
        }
    }
}
