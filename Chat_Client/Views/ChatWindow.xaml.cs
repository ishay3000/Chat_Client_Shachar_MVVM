using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using Chat_Client.Annotations;
using Chat_Client.Messages;

namespace Chat_Client.Views
{
    class ChatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<TextMessage> _textMessages = new ObservableCollection<TextMessage>();

        public ObservableCollection<TextMessage> TextMessages
        {
            get => _textMessages;
            set
            {
                this._textMessages = value;
                OnPropertyChanged();
            }
        }

        public ChatViewModel()
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public static ObservableCollection<TextMessage> Messages { get; set; } = new ObservableCollection<TextMessage>();

        public ChatWindow()
        {
            InitializeComponent();
            BindingOperations.EnableCollectionSynchronization(Messages, Messages.GetType());
            DataContext = this;
            new Thread(async () => await Client.Client.Instance.ReadingThread()).Start();
            //UnicastMessage sendMessage = new UnicastMessage("efsd", "");

        }

        private async void BtnSend_OnClick(object sender, RoutedEventArgs e)
        {
            string message = TbMessage.Text;
            string receiver = TbReceiver.Text;
            UnicastMessage sendMessage = new UnicastMessage(message, receiver);
            Messages.Add(sendMessage);
            await Client.Client.Instance.SendTextAsync(sendMessage);
        }
    }
}
