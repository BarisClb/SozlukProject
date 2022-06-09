using SozlukProject.Domain.Entities;
using SozlukProject.Service.Dtos.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Shared
{
    public class DiscussionPageReadDto
    {
        public DiscussionReadDto Discussion { get; set; }
        public ICollection<CommentReadDto>? Comments { get; set; }
        public ICollection<UserDiscussionPageReadDto>? Users { get; set; }
        public ICollection<VoteReadDto>? Votes { get; set; }

        public DiscussionPageReadDto(DiscussionReadDto discussion, ICollection<CommentReadDto>? comments, ICollection<UserDiscussionPageReadDto>? users, ICollection<VoteReadDto>? votes)
        {
            Discussion = discussion;
            Comments = comments;
            Users = users;
            Votes = votes;
        }
    }
}
