using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Chat_Client.Messages
{
    public enum EMessageTypes
    {
        LOGIN,
        REGISTER,

        UNICAST,
        BROADCAST
    }
    public class BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EMessageTypes MessageType { get; set; }

        public string Guid { get; set; } 

        public BaseMessage(EMessageTypes messageType)
        {
            MessageType = messageType;
            Guid = System.Guid.NewGuid().ToString();
        }
    }
}
