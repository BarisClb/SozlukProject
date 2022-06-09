using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Read
{
    public class UserDiscussionPageReadDto : BaseEntityReadDto
    {
        public string Name { get; set; }
        public bool Admin { get; set; }
        public bool Banned { get; set; }
    }
}
