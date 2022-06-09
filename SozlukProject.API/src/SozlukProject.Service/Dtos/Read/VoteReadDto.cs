using SozlukProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Read
{
    public class VoteReadDto : BaseEntityReadDto
    {
        public VoteType VoteType { get; set; }

        //// Relations
        // Comment the User Voted
        public int CommentId { get; set; }

        // User who Voted
        public int UserId { get; set; }
    }
}
