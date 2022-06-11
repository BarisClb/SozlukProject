using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Domain.Responses;
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
    public class VoteService : BaseService<Vote, VoteCreateDto, VoteUpdateDto, VoteReadDto>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;

        public VoteService(IVoteRepository voteRepository, IMapper mapper) : base(voteRepository, mapper)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
        }


        public async Task<BaseResponse> GetSortedVoteList(EntityListSortValues sortValues, string? byEntity = null, int? byEntityId = null)
        {
            // Check if there is an OrderBy in sortValues, if there is, implement it. If not, use the default 'Id'
            //Expression<Func<Vote, string>> orderByPredicate = c => c.UserId;
            Expression<Func<Vote, object>>? orderByKeySelector = sortValues.OrderBy switch
            {
                "Comment" => vote => vote.CommentId,
                "User" => vote => vote.UserId,
                _ => null,
            };

            Expression<Func<Vote, bool>>? entitiesByEntityPredicate = byEntity switch
            {
                "Comment" => vote => vote.CommentId == byEntityId,
                "User" => vote => vote.UserId == byEntityId,
                _ => null
            };


            return await GetSortedEntityList(sortValues, searchWordPredicate: null, orderByKeySelector, entitiesByEntityPredicate);
        }

        public async Task<BaseResponse> UpdateVote(VoteUpdateDto voteUpdateDto)
        {
            // First of all, we get and check if the entity exist
            Vote vote = await GetAndCheckEntityById(voteUpdateDto.Id, "Vote");

            // Check if the Comment is being updated.
            if (voteUpdateDto.DiscussionId != null)
                vote.CommentId = (int)voteUpdateDto.CommentId;

            // Check if the User is being updated.
            if (voteUpdateDto.UserId != null)
                vote.UserId = (int)voteUpdateDto.UserId;


            return await UpdateEntity(vote);
        }

        public async Task<FailResponse> UpdateVote()
        {
            return new FailResponse("Use CreateVote instead of UpdateVote. No need for Vote.Id, it already checks if the Vote exist, with CommentId and UserId info.");
        }

        public async Task<FailResponse> DeleteVote()
        {
            return new FailResponse("Deleting Votes is not supported. Use CreateVote to 'Un-Vote'.");
        }
    }
}
