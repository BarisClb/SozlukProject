using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Account
{
    public class AccountResetPasswordDto
    {
        public string Email { get; set; }
        public int ForgotPasswordCode { get; set; }
        public string NewPassword { get; set; }
    }
}
