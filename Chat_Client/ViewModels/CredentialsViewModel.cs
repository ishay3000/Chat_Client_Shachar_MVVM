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
        private RelayCommand _transferLoginCommand;
        private RelayCommand _transferSignUpCommand;

        #endregion

        public CredentialsViewModel()
        {
            _loginCredentialsModel = new CredentialsModel();
            _loginCommand = new RelayCommand(Login, CanExecuteCredentials);
            _registerCommand = new RelayCommand(SignUp, CanExecuteCredentials);
            _transferLoginCommand = new RelayCommand(TransferLogin, o => true);
            _transferSignUpCommand = new RelayCommand(TransferSignUp, o => true);
        }

        private void TransferLogin()
        {
            FrameManager.MovePage(ControlsTitles.Login);
        }

        private void TransferSignUp()
        {
            FrameManager.MovePage(ControlsTitles.SignUp);
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
                MessageBox.Show("Logged in!", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameManager.MovePage(ControlsTitles.Chat);
                new Thread(async () => await Client.Client.Instance.ReadingThread()).Start();
            }

            else
            {
                MessageBox.Show("Couldn't log in...", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SignUp()
        {
            bool hasRegistered = await Client.Client.Instance.SendCredentialsAsyncTask(new AuthMessage(EMessageTypes.REGISTER, Username, Password));
            if (hasRegistered)
            {
                MessageBox.Show("Registered successfully!", "Registration", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameManager.MovePage(ControlsTitles.Login);
                new Thread(async () => await Client.Client.Instance.ReadingThread()).Start();
            }

            else
            {
                MessageBox.Show("Couldn't register...", "Registration", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        public RelayCommand TransferLoginCommand
        {
            get => _transferLoginCommand;
            set => _transferLoginCommand = value;
        }

        public RelayCommand TransferSignUpCommand
        {
            get => _transferSignUpCommand;
            set => _transferSignUpCommand = value;
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
