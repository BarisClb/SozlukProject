using SozlukProject.WebAPI.Models.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.WebAPI.Models.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public int VoteRating { get; set; } = 0;

        //// Relations
        // Discussion where the Comment is written
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }

        // User who Commented
        public int UserId { get; set; }
        public User User { get; set; }

        // Votes of the Comment
        public ICollection<Vote>? Votes { get; set; }
    }
}
