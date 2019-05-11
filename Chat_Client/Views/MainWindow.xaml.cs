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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chat_Client.Messages;
using Chat_Client.Validation;
using Newtonsoft.Json;

namespace Chat_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnRegister_OnClick(object sender, RoutedEventArgs e)
        {
            string uname = TbUsername.Text;
            string pass = TbPassword.Password;

            string errors = CredentialsValidation.ValidateCredentials(uname, pass);
            if (errors != string.Empty)
            {
                MessageBox.Show(errors);
            }

            else
            {
                string json = JsonConvert.SerializeObject(new BaseMessage(EMessageTypes.REGISTER));

                // register to server
                bool hasRegistered = await Client.Client.Instance.SendCredentialsAsyncTask(new AuthMessage(EMessageTypes.REGISTER, uname, pass));

                if (hasRegistered)
                {
                    MessageBox.Show("Registered successfully!");
                    //new LoginWindow().Show();
                    // TODO transfer to messages window
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Couldn't register");
                }
            }
        }
    }
}
