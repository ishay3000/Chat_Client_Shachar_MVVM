using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chat_Client.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// initiates the services needed to run the client
        /// </summary>
        private void Init()
        {
           InitFrame();
           InitClient();
        }

        private void InitFrame()
        {
            FrameManager.MainFrame = MainFrame;
            FrameManager.MovePage(ControlsTitles.Login);
        }

        private void InitClient()
        {
            // TODO lookup how to terminate app from an async task
            Task.Factory.StartNew(async () => { await Client.Client.Instance.Start(); });
        }
    }
}
