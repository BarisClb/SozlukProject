using FluentValidation;
using SozlukProject.Service.Dtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.FluentValidation.User
{
    internal class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(user => user.Name);
        }
    }
}
