using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; } = false;
        public bool Active { get; set; } = false;
        public bool Banned { get; set; } = false;

        //// Relations
        // Comments by User
        public ICollection<Comment>? Comments { get; set; }

        // User Activation Code 
        public UserActivation UserActivation { get; set; }

        // Votes by User
        public ICollection<Vote>? Votes { get; set; }
    }
}
