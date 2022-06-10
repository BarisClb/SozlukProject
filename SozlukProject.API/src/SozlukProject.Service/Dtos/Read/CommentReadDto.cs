using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Read
{
    public class CommentReadDto : BaseEntityReadDto
    {
        public string Text { get; set; }
        public int VoteRating { get; set; }

        //// Relations
        // Discussion where the Comment is written
        public int DiscussionId { get; set; }

        // User who Commented
        public int UserId { get; set; }

    }
}
