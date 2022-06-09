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
        public static void CheckEmail(string email)
        {
            Regex emailRegex = new(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            if (!emailRegex.Match(email).Success)
                throw new Exception($"Invalid format (example@domain.com).");
        }

        // Checking Username format
        public static void CheckUsername(string username)
        {
            // Checking if it has spaces
            Regex singleWhiteSpace = new(" ");

            if (singleWhiteSpace.Match(username).Success)
                throw new Exception($"Invalid format (Username can't have spaces).");
        }

        // Checking Password format -> It is the same as Username format now but we can add different checks too (For example, mush have one Uppercase letter, one Lowercase letter, a Number, a Special Character, can't contain Name/Email, etc)
        public static void CheckPassword(string username)
        {
            // Checking if it has spaces
            Regex singleWhiteSpace = new(" ");

            if (singleWhiteSpace.Match(username).Success)
                throw new Exception($"Invalid format (Password can't have spaces).");
        }
    }
}
