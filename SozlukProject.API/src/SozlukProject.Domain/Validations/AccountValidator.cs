using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Validations
{
    public static class AccountValidator
    {
        public static bool CheckAdmin(string adminPass)
        {
            return adminPass == "123";
        }

        public static bool CheckEMail(string email)
        {
            Regex emailRegex = new(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            return emailRegex.Match(email).Success;
        }
    }
}
