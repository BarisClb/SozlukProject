using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Domain.Responses;
using SozlukProject.Domain.Validations;
using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Response;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class CommentService : BaseService<Comment, CommentCreateDto, CommentUpdateDto, CommentResponseDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        private readonly IDiscussionRepository _discussionRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IDiscussionRepository discussionRepository, IUserRepository userRepository) : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;

            _discussionRepository = discussionRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> GetSortedCommentList(EntityListSortValues sortValues, string? byEntity = null, int? byEntityId = null)
        {
            // Check if there is a SearchWord and create a Predicate if there is.
            Expression<Func<Comment, bool>>? searchWordPredicate = null;
            if (!string.IsNullOrWhiteSpace(sortValues.SearchWord))
                searchWordPredicate = c => c.Text.Contains(sortValues.SearchWord);

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

        public async Task<BaseResponse> CreateComment(CommentCreateDto commentCreateDto)
        {
            // We edit the Text, First Trim() and then Replace multiple whitespaces with single whitespace.
            commentCreateDto.Text = EntityValidator.TrimAndClearMultipleWhitespaces(commentCreateDto.Text);

            // Check if Discussion exist.
            if (await _discussionRepository.GetByIdAsync(commentCreateDto.DiscussionId, false) == null)
                return new FailResponse("Discussion does not exist.");

            // Check if User exist.
            if (await _userRepository.GetByIdAsync(commentCreateDto.UserId, false) == null)
                return new FailResponse("User does not exist.");


            return await CreateEntity(commentCreateDto);
        }

        public async Task<BaseResponse> UpdateComment(CommentUpdateDto commentUpdateDto)
        {
            // First of all, we check if entity exist
            Comment comment = await _commentRepository.GetByIdAsync(commentUpdateDto.Id);
            if (comment == null)
                return new FailResponse("Comment does not exist.");

            // We edit the Text (if its being updated), First Trim() and then Replace multiple whitespaces with single whitespace.
            if (commentUpdateDto.Text != null)
                comment.Text = EntityValidator.TrimAndClearMultipleWhitespaces(commentUpdateDto.Text);
            
            // Check if the Discussion exist.
            if (commentUpdateDto.DiscussionId != null)
            {
                if (await _discussionRepository.GetByIdAsync((int)commentUpdateDto.DiscussionId, false) == null)
                    return new FailResponse("Discussion does not exist.");
                comment.DiscussionId = (int)commentUpdateDto.DiscussionId;
            }
                
            // Check if the User exist.
            if (commentUpdateDto.UserId != null)
            {
                if (await _userRepository.GetByIdAsync((int)commentUpdateDto.UserId, false) == null)
                    return new FailResponse("User does not exist.");

                comment.UserId = (int)commentUpdateDto.UserId;
            }
                

            return await UpdateEntity(comment);
        }
    }
}
