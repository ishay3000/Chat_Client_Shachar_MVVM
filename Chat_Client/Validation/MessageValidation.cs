using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_Client.Messages;

namespace Chat_Client.Validation
{
    static class MessageValidation
    {
        public static Tuple<bool, Type> ValidateMessage(string content, string receiver, bool? isBroadcast)
        {
            Type retType;
            bool isValid;

            if (isBroadcast != null && isBroadcast == true)
            {
                isValid = ValidateBroadcast(content, isBroadcast);
                retType = typeof(TextMessage);
            }

            else
            {
                isValid = ValidateUnicast(content, receiver);
                retType = typeof(UnicastMessage);
            }
            return new Tuple<bool, Type>(isValid, retType);
        }

        private static bool ValidateUnicast(string content, string receiver)
        {
            return IsValidProperty(new[] { content, receiver }, 128);
        }

        private static bool IsValidProperty(string[] properties, int maxLen)
        {
            return properties.All(property => !string.IsNullOrEmpty(property) && property.Length < maxLen);
        }

        private static bool ValidateBroadcast(string content, bool? isBroadcasted)
        {
            return IsValidProperty(new[] { content }, 128) && (isBroadcasted != null && isBroadcasted == true);
        }
    }
}
