using SozlukProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Common
{
    public class DiscussionPageDto
    {
        public Discussion Discussion { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Vote>? Votes { get; set; }
    }
}
