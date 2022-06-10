using FluentValidation;
using SozlukProject.Service.Dtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.FluentValidation.Vote
{
    internal class VoteCreateValidator : AbstractValidator<VoteCreateDto>
    {
        public VoteCreateValidator()
        { }
    }
}
