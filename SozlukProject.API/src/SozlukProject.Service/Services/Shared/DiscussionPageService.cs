using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Responses;
using SozlukProject.Domain.Validations.Common;
using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Shared;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class DiscussionPageService
    {
        private readonly CommentService _commentService;
        private readonly DiscussionService _discussionService;
        private readonly UserService _userService;
        private readonly VoteService _voteService;

        private readonly IMapper _mapper;

        public DiscussionPageService(CommentService commentService, DiscussionService discussionService, UserService userService, VoteService voteService, IMapper mapper)
        {
            _commentService = commentService;
            _discussionService = discussionService;
            _userService = userService;
            _voteService = voteService;

            _mapper = mapper;
        }


        public async Task<SuccessfulResponse<DiscussionPageReadDto>> GetDiscussionPage(int discussionId, EntityListSortValues sortValues)
        {
            // Get Discussion
            DiscussionReadDto discussion = _mapper.Map<Discussion, DiscussionReadDto>(await _discussionService.GetAndCheckEntityById(discussionId));

            // Get Comments
            Expression<Func<Comment, bool>> commentListByDiscussionPredicate = comment => comment.DiscussionId == discussionId;
            SortedResponse<IList<CommentReadDto>, EntityListSortValues> commentResponse = await _commentService.GetSortedEntityList(sortValues, null, null, commentListByDiscussionPredicate);

            IList<CommentReadDto> comments = commentResponse.Data;

            // Get Users of the Comments
            List<int> userIds = new();
            List<int> commentIds = new();
            List<UserDiscussionPageReadDto> users = new();

            // First, we get the IdList and UserIdList of the Comments
            foreach (var comment in comments)
            {
                userIds.Add(comment.UserId);
                commentIds.Add(comment.Id);
            }

            // We use Distinct to not get the same Users
            userIds = userIds.Distinct().ToList();
            foreach (int id in userIds)
            {
                var user = await _userService.GetAndCheckEntityById(id);
                users.Add(_mapper.Map<User, UserDiscussionPageReadDto>(user));
            }

            // Get Votes of the Comments
            List<VoteReadDto> votes = _voteService.GetEntitiesDtoWhere(vote => commentIds.Contains(vote.CommentId)).ToList();


            return new SuccessfulResponse<DiscussionPageReadDto>(new DiscussionPageReadDto(discussion, comments, users, votes));
        }

        public async Task<SuccessfulResponse<CommentReadDto>> CreateComment(CommentCreateDto commentCreateDto)
        {
            // First, check if Discussion exist.
            Discussion discussion = await _discussionService.GetAndCheckEntityById(commentCreateDto.DiscussionId);

            // Then we edit the Text, First Trim() and then Replace multiple whitespaces with single whitespace.
            commentCreateDto.Text = CommonValidator.TrimAndClearMultipleWhitespaces(commentCreateDto.Text);

            // Intercepting the response
            SuccessfulResponse<CommentReadDto> response = await _commentService.CreateEntity(commentCreateDto);

            // Triggering the Update on Discussion
            await UpdateDiscussionCommentCount(discussion);


            return response;
        }

        public async Task<SuccessfulResponse<int>> DeleteComment(int commentId)
        {
            // First, get the Comment
            Comment comment = await _commentService.GetAndCheckEntityById(commentId);

            // First, check if Discussion exist.
            Discussion discussion = await _discussionService.GetAndCheckEntityById(comment.DiscussionId);

            // Intercepting the response
            SuccessfulResponse<int> response = await _commentService.DeleteEntity(commentId);

            // Triggering the Update on Discussion
            await UpdateDiscussionCommentCount(discussion);


            return response;
        }

        public async Task<BaseResponse> CreateVote(VoteCreateDto voteCreateDto)
        {
            Vote vote = await _voteService.GetEntityWhere(vote => vote.UserId == voteCreateDto.UserId && vote.CommentId == voteCreateDto.CommentId);

            if (vote != null)
            {
                vote.VoteType = voteCreateDto.VoteType;
                return await UpdateVote(_mapper.Map<Vote, VoteUpdateDto>(vote));
            }


            return await _voteService.CreateEntity(voteCreateDto);
        }

        public async Task<BaseResponse> UpdateVote(VoteUpdateDto voteUpdateDto)
        {


            return new FailResponse("Not implemented");
        }




        // Helpers

        public async Task UpdateDiscussionCommentCount(Discussion discussion)
        {
            int commentCount = _commentService.GetEntitiesWhere(comment => comment.DiscussionId == discussion.Id).Count();
            discussion.CommentCount = commentCount;
            await _discussionService.UpdateEntity(discussion);
        }

        public async Task UpdateCommentVoteRating()
        {

        }
    }
}
