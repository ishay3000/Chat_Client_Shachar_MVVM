using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chat_Client.Messages
{
    public class TextMessage : BaseMessage
    {
        public string Content { get; set; }
        private string _sender;

        public string Sender
        {
            get => MyMessage ? "You said" : (MessageType == EMessageTypes.BROADCAST ? "Broadcast by " + _sender : _sender);
            set => _sender = value;
        }

        [JsonIgnore]
        public bool MyMessage { get; set; } = false;

        private string _alignment;
        [JsonIgnore]
        public string Alignment
        {
            get => MyMessage ? "Right" : "Left";
            set => _alignment = value;
        }

        private string _time;
        [JsonIgnore]
        public string Time
        {
            get => DateTime.Now.ToShortTimeString();
            set => _time = value;
        }

        public TextMessage(EMessageTypes messageType, string content) : base(messageType)
        {
            Content = content;
        }
    }
}
