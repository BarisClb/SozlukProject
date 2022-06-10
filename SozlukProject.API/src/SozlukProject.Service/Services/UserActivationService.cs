using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class UserActivationService : BaseService<UserActivation, UserActivationCreateDto, UserActivationUpdateDto, UserActivationReadDto>
    {
        public UserActivationService(IUserActivationRepository userActivationRepository, IMapper mapper) : base(userActivationRepository, mapper)
        { }
    }
}
