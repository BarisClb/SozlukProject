using SozlukProject.WebAPI.Models.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.WebAPI.Models.Entities
{
    public class UserActivation : BaseEntity
    {
        // Id of this Entity is the ForeignKey => UserId.
        public User User { get; set; }
        public int? ActivationCode { get; set; }
        public int? ResetPasswordCode { get; set; }
    }
}
