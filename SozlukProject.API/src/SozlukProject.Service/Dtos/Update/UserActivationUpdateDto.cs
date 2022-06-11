using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Update
{
    public class UserActivationUpdateDto : BaseEntityUpdateDto
    {
        public int? ActivationCode { get; set; }
        public int? ResetPasswordCode { get; set; }
    }
}
