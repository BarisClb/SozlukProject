using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Domain.Responses;
using SozlukProject.Domain.Validations.Common;
using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class CommentService : BaseService<Comment, CommentCreateDto, CommentUpdateDto, CommentReadDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper) : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }


        public async Task<BaseResponse> GetSortedCommentList(EntityListSortValues sortValues, string? byEntity = null, int? byEntityId = null)
        {
            // Check if there is a SearchWord, create a Predicate if there is one.
            Expression<Func<Comment, bool>>? searchWordPredicate = null;
            if (!string.IsNullOrWhiteSpace(sortValues.SearchWord))
                searchWordPredicate = comment => comment.Text.Contains(sortValues.SearchWord);

            // Check if there is an OrderBy in sortValues, if there is, implement it. If not, use the default 'Id'
            //Expression<Func<Comment, string>> orderByPredicate = c => c.UserId;
            Expression<Func<Comment, object>>? orderByKeySelector = sortValues.OrderBy switch
            {
                "Discussion" => comment => comment.DiscussionId,
                "User" => comment => comment.UserId,
                _ => null,
            };

            Expression<Func<Comment, bool>>? entitiesByEntityPredicate = byEntity switch
            {
                "Discussion" => comment => comment.DiscussionId == byEntityId,
                "User" => comment => comment.UserId == byEntityId,
                _ => null
            };


            return await GetSortedEntityList(sortValues, searchWordPredicate, orderByKeySelector, entitiesByEntityPredicate);
        }

        public async Task<BaseResponse> UpdateComment(CommentUpdateDto commentUpdateDto)
        {
            // First of all, we get and check if the entity exist
            Comment comment = await GetAndCheckEntityById(commentUpdateDto.Id, "Comment");

            // We edit the Text (if its being updated), First Trim() and then Replace multiple whitespaces with single whitespace.
            if (commentUpdateDto.Text != null)
                comment.Text = CommonValidator.TrimAndClearMultipleWhitespaces(commentUpdateDto.Text);

            // Check if the Discussion exist.
            if (commentUpdateDto.DiscussionId != null)
            {
                comment.DiscussionId = (int)commentUpdateDto.DiscussionId;
            }

            // Check if the User exist.
            if (commentUpdateDto.UserId != null)
            {
                comment.UserId = (int)commentUpdateDto.UserId;
            }


            return await UpdateEntity(comment);
        }
    }
}
