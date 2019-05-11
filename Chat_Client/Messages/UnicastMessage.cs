using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Messages
{
    class UnicastMessage : TextMessage
    {

        public string Receiver { get; set; }

        public UnicastMessage(string content, string receiver) : base(EMessageTypes.UNICAST, content)
        {
            Receiver = receiver;
        }
    }
}
