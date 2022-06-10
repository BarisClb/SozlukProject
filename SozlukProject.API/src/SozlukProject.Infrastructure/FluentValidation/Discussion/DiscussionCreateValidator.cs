using FluentValidation;
using SozlukProject.Service.Dtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.FluentValidation.Discussion
{
    public class DiscussionCreateValidator : AbstractValidator<DiscussionCreateDto>
    {
        public DiscussionCreateValidator()
        {
            RuleFor(discussion => discussion.Title);
        }
    }
}
