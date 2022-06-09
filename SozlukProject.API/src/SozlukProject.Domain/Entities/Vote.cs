using SozlukProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Entities
{
    public class Vote : BaseEntity
    {
        public VoteType VoteType { get; set; }

        //// Relations
        // Comment the User Voted
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        // User who Voted
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
