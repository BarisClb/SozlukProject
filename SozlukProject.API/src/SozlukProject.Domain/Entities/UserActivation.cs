using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Entities
{
    public class UserActivation : BaseEntity
    {
        // Id of this Entity is the ForeignKey => UserId.
        public User User { get; set; }
        public int? ActivationCode { get; set; }
        public int? ResetPasswordCode { get; set; }
    }
}
