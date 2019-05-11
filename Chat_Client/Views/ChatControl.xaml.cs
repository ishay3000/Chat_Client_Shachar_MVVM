using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat_Client.Views
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        public ChatControl()
        {
            InitializeComponent();
            DataContext = ViewModels.ChatViewModel.Instance;
            BindingOperations.EnableCollectionSynchronization(ViewModels.ChatViewModel.Instance.TextMessages, ViewModels.ChatViewModel.Instance.TextMessages.GetType());

            // TODO lookup how to execute when user control is visible
            //new Thread(async () => await Client.Client.Instance.ReadingThread()).Start();
        }
    }
}
