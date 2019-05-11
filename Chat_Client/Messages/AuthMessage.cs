using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Messages
{
    class AuthMessage : BaseMessage
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public AuthMessage(EMessageTypes messageType, string username, string password) : base(messageType)
        {
            Username = username;
            Password = password;
        }
    }
}
