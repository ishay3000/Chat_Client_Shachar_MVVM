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
using Chat_Client.Messages;
using Chat_Client.Validation;
using Chat_Client.Views;
using Newtonsoft.Json;

namespace Chat_Client
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            //UnicastMessage message = new UnicastMessage("my content", "George");
            //string json = JsonConvert.SerializeObject(message);

            //try
            //{
            //    TextMessage deserialized = JsonConvert.DeserializeObject<TextMessage>(json);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

        }

        private async void BtnSignIn_OnClick(object sender, RoutedEventArgs e)
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
                string json = JsonConvert.SerializeObject(new BaseMessage(EMessageTypes.LOGIN));

                // register to server
                bool hasSignedIn = await Client.Client.Instance.SendCredentialsAsyncTask(new AuthMessage(EMessageTypes.LOGIN, uname, pass));

                if (hasSignedIn)
                {
                    MessageBox.Show("Signed in successfully!");
                    new ChatWindow().Show();
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Couldn't sign in");
                }
            }
        }
    }
}
