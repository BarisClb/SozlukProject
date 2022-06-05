﻿using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Service.Dtos.Response;
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
            CreateMap<Discussion, DiscussionResponseDto>();
        }
    }
}
