using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Entities
{
    public class Discussion : BaseEntity
    {
        public string Title { get; set; }
        // The purpose of this prop is to trigger DateUpdated of the Discussion after it gets or loses a Comment. This way, we can list Discussions with the DateUpdated prop.
        public int CommentCount { get; set; } = 0;

        //// Relations
        // Comments of the Discussion
        public ICollection<Comment>? Comments { get; set; }

        // Votes of the Comments under this Discussion
        public ICollection<Vote>? Votes { get; set; }
    }
}
