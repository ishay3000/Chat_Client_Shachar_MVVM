using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Chat_Client.Messages;
using Chat_Client.Views;
using Newtonsoft.Json;
using ChatViewModel = Chat_Client.ViewModels.ChatViewModel;

namespace Chat_Client.Client
{
    class Client
    {
        public static Client Instance = new Client();

        #region Members

        private readonly TcpClient _client = new TcpClient();
        private NetworkStream _networkStream;
        private bool _isReading = false;
        private const string Ip = "127.0.0.1";
        private const int Port = 8090;

        #endregion

        private Client()
        {
            //Task.WhenAll(Start()).ConfigureAwait(false);
            //Start();
        }

        public async Task<bool> Start()
        {
            try
            {
                await _client.ConnectAsync(Ip, Port);
                _networkStream = _client.GetStream();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task ReadingThread()
        {
            _isReading = true;

            while (_isReading)
            {
                var response = await ReadAsyncTask();
                dynamic res;
                try
                {
                    res = JsonConvert.DeserializeObject(response);
                    var status = res["Status"];
                    var Guid = res["Guid"];
                    if (status != null)
                    {
                        var messages = ChatViewModel.Instance.TextMessages;
                        if (status?.ToString() == "OK")
                        {

                        }
                        else
                        {
                            MessageBox.Show(res["Cause"]?.ToString());
                            var msg = messages.FirstOrDefault(m => m.Guid == Guid.ToString());
                            int index = messages.IndexOf(msg);

                            if (index >= 0)
                            {
                                messages.RemoveAt(index);
                            }
                        }
                    }
                    else
                    {
                        TextMessage message = JsonConvert.DeserializeObject<TextMessage>(response);
                        ChatViewModel.Instance.TextMessages.Add(message);
                    }
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    _isReading = false;
                }
            }
        }

        public async Task SendTextAsync(TextMessage message)
        {
            await this.SendAsyncTask(message);
            //string read = await this.ReadAsyncTask();
        }

        public async Task<string> ReadAsyncTask()
        {
            byte[] response = new byte[256];
            await _networkStream.ReadAsync(response, 0, response.Length);

            return Encoding.UTF8.GetString(response);
        }

        public async Task SendAsyncTask(BaseMessage message)
        {
            byte[] request = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            await _networkStream.WriteAsync(request, 0, request.Length);
        }

        public async Task<bool> SendCredentialsAsyncTask(AuthMessage message)
        {
            await SendAsyncTask(message);
            string response = await ReadAsyncTask();

            dynamic result = JsonConvert.DeserializeObject(response);
            if (result["Status"] != null && result["Status"].ToString() == "OK")
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
