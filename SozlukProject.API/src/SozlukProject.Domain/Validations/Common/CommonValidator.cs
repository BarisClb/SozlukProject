using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Validations.Common
{
    public static class CommonValidator
    {
        public static bool CheckSingleWhiteSpace(string str)
        {
            Regex singleWhiteSpace = new(" ");

            return singleWhiteSpace.Match(str).Success;
        }

        public static bool CheckDoubleWhiteSpace(string str)
        {
            Regex singleWhiteSpace = new("  ");

            return singleWhiteSpace.Match(str).Success;
        }

        public static string TrimAndClearMultipleWhitespaces(string str)
        {
            return Regex.Replace(str.Trim(), @"\s+", " ");
        }

        public static string TrimAndCheckIfEmpty(string str, string property = null)
        {
            str = str.Trim();

            if (str == "")
                throw new Exception($"Invalid format ({property ?? "Property"} can't be empty).");

            return str;
        }
    }
}
