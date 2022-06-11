using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Account
{
    public class AccountChangePasswordDto
    {
        public string Account { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
