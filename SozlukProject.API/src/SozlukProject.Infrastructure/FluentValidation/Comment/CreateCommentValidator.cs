using FluentValidation;
using SozlukProject.Service.Dtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.FluentValidation.Comment
{
    public class CreateCommentValidator : AbstractValidator<CommentCreateDto>
    {
        public CreateCommentValidator()
        {
            RuleFor(comment => comment.Text).MaximumLength(5);
        }
    }
}
