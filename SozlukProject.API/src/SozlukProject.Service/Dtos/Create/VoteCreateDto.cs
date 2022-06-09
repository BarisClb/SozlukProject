using SozlukProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Create
{
    public class VoteCreateDto : BaseEntityCreateDto
    {
        public VoteType VoteType { get; set; }

        //// Relations
        // Comment the User Voted
        public int CommentId { get; set; }

        // User who Voted
        public int UserId { get; set; }
    }
}
