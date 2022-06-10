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
        // Checking Admin
        public static bool CheckAdminPassword(string adminPass)
        {
            return adminPass == "123";
        }

        // Checking Email format
        public static bool CheckEmail(string email)
        {
            if (!EmailFormat(email))
                throw new Exception($"Invalid format (example@domain.com).");


            return true;
        }

        // Checking Username format
        public static bool CheckUsername(string username)
        {
            if (SingleWhiteSpace(username))
                throw new Exception($"Invalid format (Username can't have spaces).");

            if (EmailFormat(username))
                throw new Exception($"Invalid format (Username can't be in Email format (example@domain.com)).");


            return true;
        }

        public static bool CheckPassword(string username)
        {
            if (SingleWhiteSpace(username))
                throw new Exception($"Invalid format (Password can't have spaces).");


            return true;
        }


        // Helpers
        private static bool EmailFormat(string str)
        {
            Regex emailRegex = new(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            return emailRegex.Match(str).Success;
        }

        private static bool SingleWhiteSpace(string str)
        {
            // Checking if it has any spaces
            Regex singleWhiteSpace = new(" ");

            return singleWhiteSpace.Match(str).Success;
        }
    }
}
