using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Validation
{
    public static class CredentialsValidation
    {

        public static bool ValidateCreds(string username, string password)
        {
            return IsValidString(username) && IsValidString(password);
        }
        public static String ValidateCredentials(string uname, string pass)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!IsValidString(uname))
            {
                stringBuilder.Append("You must enter a valid username\n");
            }

            if (!IsValidString(pass))
            {
                stringBuilder.Append("You must enter a valid password\n");
            }

            return stringBuilder.ToString();
        }

        private static bool IsValidString(string property)
        {
            return !string.IsNullOrEmpty(property) && property.Length < 15;
        }
    }
}
