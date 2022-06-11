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
        private readonly IUserActivationRepository _userActivationRepository;

        public UserActivationService(IUserActivationRepository userActivationRepository, IMapper mapper) : base(userActivationRepository, mapper)
        {
            _userActivationRepository = userActivationRepository;
        }


        public async Task<UserActivation> CreateUserActivation(UserActivation userActivation)
        {
            await _userActivationRepository.AddAsync(userActivation);
            await _userActivationRepository.SaveChangesAsync();


            return userActivation;
        }
    }
}
