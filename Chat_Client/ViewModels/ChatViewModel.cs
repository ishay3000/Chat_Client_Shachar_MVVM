using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_Client.Messages;
using Chat_Client.Validation;

namespace Chat_Client.ViewModels
{
    class ChatViewModel : BaseViewModel
    {
        #region Members

        private ObservableCollection<TextMessage> _textMessages;
        public static ChatViewModel Instance { get; } = new ChatViewModel();
        private RelayCommand _sendCommand;
        private string _messageContent;
        private string _receiver;
        private bool? _isBroadcastChecked;
        private Type _messageType;

        #endregion


        public ChatViewModel()
        {
            TextMessages = new ObservableCollection<TextMessage>();
            _sendCommand = new RelayCommand(SendMessage, CanExecuteSendMessage);
            _isBroadcastChecked = false;
        }

        private bool CanExecuteSendMessage(object obj)
        {
            var res = MessageValidation.ValidateMessage(_messageContent, _receiver, _isBroadcastChecked);
            _messageType = res.Item2;

            return res.Item1;
        }

        private async void SendMessage()
        {
            TextMessage message = (_messageType == typeof(UnicastMessage)
                ? new UnicastMessage(_messageContent, _receiver)
                : new TextMessage(EMessageTypes.BROADCAST, _messageContent));

            message.MyMessage = true;

            await Client.Client.Instance.SendTextAsync(message);
            TextMessages.Add(message);
        }

        public ObservableCollection<TextMessage> TextMessages 
        {
            get => _textMessages;
            set => _textMessages = value;
        }

        public RelayCommand SendCommand
        {
            get => _sendCommand;
            set => _sendCommand = value;
        }

        public string MessageContent
        {
            get => _messageContent;
            set
            {
                _messageContent = value;
                OnPropertyChanged();
            }
        }

        public string Receiver
        {
            get => _receiver;
            set
            {
                _receiver = value; 
                OnPropertyChanged();
            }
        }

        public bool? IsBroadcastChecked
        {
            get => _isBroadcastChecked;
            set
            {
                _isBroadcastChecked = value; 
                OnPropertyChanged();
            }
        }
    }
}
