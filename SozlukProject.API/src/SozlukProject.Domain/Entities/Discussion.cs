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

        //// Relations
        // Comments of the Discussion
        public ICollection<Comment>? Comments { get; set; }

        // Votes of the Comments under this Discussion
        public ICollection<Vote>? Votes { get; set; }
    }
}
