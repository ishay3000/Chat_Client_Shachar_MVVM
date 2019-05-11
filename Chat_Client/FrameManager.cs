using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Chat_Client.Views;

namespace Chat_Client
{

    public enum ControlsTitles
    {
        Login,
        SignUp,
        Chat
    }
    static class FrameManager
    {
        public static Frame MainFrame { get; set; }
        private static readonly Dictionary<ControlsTitles, UserControl> ControlsDictionary;

        static FrameManager()
        {
            ControlsDictionary = new Dictionary<ControlsTitles, UserControl>();
            InitDictionary();
        }

        /// <summary>
        /// initiates the user controls dict
        /// </summary>
        private static void InitDictionary()
        {
           ControlsDictionary[ControlsTitles.Login] = new LoginControl();
           ControlsDictionary[ControlsTitles.SignUp] = new SignUpControl();
           ControlsDictionary[ControlsTitles.Chat] = new ChatControl();
        }

        /// <summary>
        /// Moves the frame to a different page
        /// </summary>
        /// <param name="pageTitle">the page title</param>
        public static void MovePage(ControlsTitles pageTitle)
        {
            if (ControlsDictionary.ContainsKey(pageTitle))
            {
                var control = ControlsDictionary[pageTitle];
                MainFrame.Content = control;
                //var controlTuple = ControlsDictionary[pageTitle];
                //// change page
                //MainFrame.Content = controlTuple.Item1;
                //// change title
                //WindowViewModel.INSTANCE.WindowTitle = controlTuple.Item2;
            }
        }
    }
}
