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
            DiscussionReadDto discussion = await _discussionService.GetAndCheckEntityByIdDto(discussionId);

            // Get Comments
            Expression<Func<Comment, bool>> commentListByDiscussionPredicate = comment => comment.DiscussionId == discussionId;
            SortedResponse<IList<CommentReadDto>, EntityListSortValues> commentResponse = await _commentService.GetSortedEntityList(sortValues, null, null, commentListByDiscussionPredicate);

            IList<CommentReadDto> comments = commentResponse.Data;

            // Get Users of the Comments
            List<int> userIds = new();
            List<int> commentIds = new();
            List<UserDiscussionPageReadDto> users = new();

            // First, we get the IdList and UserIdList of the Comments
            foreach (CommentReadDto comment in comments)
            {
                userIds.Add(comment.UserId);
                commentIds.Add(comment.Id);
            }

            // We use Distinct to not get the same Users
            userIds = userIds.Distinct().ToList();
            foreach (int id in userIds)
            {
                User user = await _userService.GetAndCheckEntityById(id, "User");
                users.Add(_mapper.Map<User, UserDiscussionPageReadDto>(user));
            }

            // Get Votes of the Comments
            List<VoteReadDto> votes = _voteService.GetEntitiesWhereDto(vote => commentIds.Contains(vote.CommentId)).ToList();


            return new SuccessfulResponse<DiscussionPageReadDto>(new DiscussionPageReadDto(discussion, comments, users, votes));
        }

        public async Task<SuccessfulResponse<CommentReadDto>> CreateComment(CommentCreateDto commentCreateDto)
        {
            // First, check if Discussion exist.
            Discussion discussion = await _discussionService.GetAndCheckEntityById(commentCreateDto.DiscussionId, "Discussion");

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
            Comment comment = await _commentService.GetAndCheckEntityById(commentId, "Comment");

            // First, check if Discussion exist.
            Discussion discussion = await _discussionService.GetAndCheckEntityById(comment.DiscussionId, "Discussion");

            // Intercepting the Response and Update the Discussion
            SuccessfulResponse<int> response = await _commentService.DeleteEntity(commentId, "Comment");

            // Triggering the Update on Discussion
            await UpdateDiscussionCommentCount(discussion);


            return response;
        }

        public async Task<SuccessfulResponse<VoteReadDto>> CreateOrUpdateVote(VoteCreateDto voteCreateDto)
        {
            Vote vote = await _voteService.GetEntityWhere(vote => vote.UserId == voteCreateDto.UserId && vote.CommentId == voteCreateDto.CommentId);

            Comment comment = await _commentService.GetAndCheckEntityById(voteCreateDto.CommentId, "Comment");

            // To intercept the Response and update Comment
            SuccessfulResponse<VoteReadDto> response;

            // If Vote already exist, we send it to get Updated
            if (vote != null)
            {
                // Instead of requesting a 'DeVote' action, we will make it so that if User uses an UpVote on a Comment that is already UpVoted (and vice-versa), it will get Neutralized
                if (vote.VoteType == voteCreateDto.VoteType)
                    vote.VoteType = 0;
                else
                    vote.VoteType = voteCreateDto.VoteType;

                response = await _voteService.UpdateEntity(vote);
            }
            else
                response = await _voteService.CreateEntity(voteCreateDto);

            // Triggering the Update on Comment
            await UpdateCommentVoteRating(comment);


            return response;
        }

        // Helpers

        public async Task UpdateDiscussionCommentCount(Discussion discussion)
        {
            // Number of Comments of the Discussion
            int commentCount = _commentService.GetEntitiesWhere(comment => comment.DiscussionId == discussion.Id).Count();

            // Updating the CommentCount of the Discussion
            discussion.CommentCount = commentCount;
            await _discussionService.UpdateEntity(discussion);
        }

        public async Task UpdateCommentVoteRating(Comment comment)
        {
            // Number of DownVotes of the Comment
            int downVoteCount = _voteService.GetEntitiesWhere(vote => vote.CommentId == comment.Id && vote.VoteType == Domain.Enums.VoteType.DownVote).Count();
            // Number of UpVotes of the Comment
            int upVoteCount = _voteService.GetEntitiesWhere(vote => vote.CommentId == comment.Id && vote.VoteType == Domain.Enums.VoteType.UpVote).Count();

            // Updating the VoteRating of the Comment
            comment.VoteRating = upVoteCount - downVoteCount;
            await _commentService.UpdateEntity(comment);
        }
    }
}
