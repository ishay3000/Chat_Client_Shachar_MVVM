using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Models
{
    class CredentialsModel
    {
        private string _username;
        private string _password;

        public CredentialsModel(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public CredentialsModel()
        {
        }

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }
    }
}
