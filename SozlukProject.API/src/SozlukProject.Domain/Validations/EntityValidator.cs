using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Validations
{
    public static class EntityValidator
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
    }
}
