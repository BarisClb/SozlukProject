using SozlukProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Response
{
    public class VoteResponseDto : BaseEntityResponseDto
    {
        public VoteType VoteType { get; set; }

        //// Relations
        // Comment the User Voted
        public int CommentId { get; set; }

        // Discussion where the Comment is written
        public int DiscussionId { get; set; }

        // User who Voted
        public string? UserUsername { get; set; }
        public int UserId { get; set; }
    }
}
