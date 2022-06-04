using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Update
{
    public class UserUpdateDto : BaseEntityUpdateDto
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? Admin { get; set; }
        public string? AdminPassword { get; set; }
    }
}
