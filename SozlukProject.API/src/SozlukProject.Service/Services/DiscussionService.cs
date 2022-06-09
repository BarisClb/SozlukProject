using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Domain.Responses;
using SozlukProject.Domain.Validations.Common;
using SozlukProject.Persistence.Repositories;
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
    public class DiscussionService : BaseService<Discussion, DiscussionCreateDto, DiscussionUpdateDto, DiscussionReadDto>
    {
        private readonly IDiscussionRepository _discussionRepository;
        private readonly IMapper _mapper;

        public DiscussionService(IDiscussionRepository discussionRepository, IMapper mapper) : base(discussionRepository, mapper)
        {
            _discussionRepository = discussionRepository;
            _mapper = mapper;
        }


        public async Task<BaseResponse> GetSortedDiscussionList(EntityListSortValues sortValues)
        {
            // Check if there is a SearchWord, create a Predicate if there is one.
            Expression<Func<Discussion, bool>>? searchWordPredicate = null;
            if (!string.IsNullOrWhiteSpace(sortValues.SearchWord))
                searchWordPredicate = discussion => discussion.Title.Contains(sortValues.SearchWord);


            return await GetSortedEntityList(sortValues, searchWordPredicate);
        }

        public async Task<SuccessfulResponse<DiscussionReadDto>> CreateDiscussion(DiscussionCreateDto discussionCreateDto)
        {
            discussionCreateDto.Title = CommonValidator.TrimAndClearMultipleWhitespaces(discussionCreateDto.Title);


            return await CreateEntity(discussionCreateDto);
        }

        public async Task<BaseResponse> UpdateDiscussion(DiscussionUpdateDto discussionUpdateDto)
        {
            // First of all, we get and check if the entity exist
            Discussion discussion = await GetAndCheckEntityById(discussionUpdateDto.Id);

            // We edit the Text (if its being updated), First Trim() and then Replace multiple whitespaces with single whitespace.
            if (discussionUpdateDto.Title != null)
                discussion.Title = CommonValidator.TrimAndClearMultipleWhitespaces(discussionUpdateDto.Title);


            return await UpdateEntity(discussion);
        }


        //// Helpers

        // This is triggered after a Comment is Created, the purpose is to 'Update' the discussion, so we can list them with DateUpdated
        public async void UpdateCommentCount(int discussionId, int commentCount)
        {
            // Updating the Discussion via CommentCount
            Discussion discussion = await GetAndCheckEntityById(discussionId);
            discussion.CommentCount = commentCount;


            await UpdateEntity(discussion);
        }
    }
}
