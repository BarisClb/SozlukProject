using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Update
{
    public class DiscussionUpdateDto : BaseEntityUpdateDto
    {
        public string? Title { get; set; }
        public int? CommentCount { get; set; }
    }
}
