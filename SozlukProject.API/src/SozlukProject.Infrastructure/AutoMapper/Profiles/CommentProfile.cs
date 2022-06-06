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
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            // When Creating
            CreateMap<CommentCreateDto, Comment>();

            // When Updating
            CreateMap<CommentUpdateDto, Comment>();

            // When Listing/Getting
            CreateMap<Comment, CommentResponseDto>();

        }
    }
}
