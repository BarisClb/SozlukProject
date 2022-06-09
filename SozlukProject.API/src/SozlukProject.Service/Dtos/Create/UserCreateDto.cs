using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Create
{
    public class UserCreateDto : BaseEntityCreateDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Admin { get; set; }
        public string? AdminPassword { get; set; }
        public bool Active { get; set; } = false;
    }
}
