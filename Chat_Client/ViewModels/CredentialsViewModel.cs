using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Chat_Client.Messages;
using Chat_Client.Models;
using Chat_Client.Validation;

namespace Chat_Client.ViewModels
{
    class CredentialsViewModel : BaseViewModel
    {
        #region Members

        public static BaseViewModel Instance { get; } = new CredentialsViewModel();

        private CredentialsModel _loginCredentialsModel;
        private RelayCommand _loginCommand;
        private RelayCommand _registerCommand;

        #endregion

        public CredentialsViewModel()
        {
            _loginCredentialsModel = new CredentialsModel();
            _loginCommand = new RelayCommand(Login, CanExecuteCredentials);
            _registerCommand = new RelayCommand(SignUp, CanExecuteCredentials);
        }

        private bool CanExecuteCredentials(object obj)
        {
            return CredentialsValidation.ValidateCreds(Username, Password);
        }

        private async void Login()
        {
            bool hasLogged = await Client.Client.Instance.SendCredentialsAsyncTask(new AuthMessage(EMessageTypes.LOGIN, Username, Password));
            if (hasLogged)
            {
                MessageBox.Show("Logged in!");
                FrameManager.MovePage(ControlsTitles.Chat);
                new Thread(async () => await Client.Client.Instance.ReadingThread()).Start();
            }

            else
            {
                MessageBox.Show("Couldn't log in...");
            }
        }

        private async void SignUp()
        {

        }

        public string Username
        {
            get => _loginCredentialsModel.Username;
            set
            {
                _loginCredentialsModel.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _loginCredentialsModel.Password;
            set
            {
                _loginCredentialsModel.Password = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand
        {
            get => _loginCommand;
            set => _loginCommand = value;
        }

        public RelayCommand RegisterCommand
        {
            get => _registerCommand;
            set => _registerCommand = value;
        }
    }
}
