using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestFlow.Common.Validations
{
    public static class UserValidations
    {
        public static bool IsValidUserName(string userName)
        {
            return Regex.IsMatch(userName, @"^[a-zA-Z0-9]{3,20}$");
        }

        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
        }
    }
}
