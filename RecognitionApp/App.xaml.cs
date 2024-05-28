using Microsoft.UI.Xaml;
using RecognitionApp.Model.Enums;
using System;
using System.Net;


namespace RecognitionApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow MainWindow = new();

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13 | SecurityProtocolType.Tls;
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow.Activate();
            InitializeUserSettings();
        }

        private void InitializeUserSettings()
        {
            var userID = Properties.UserSettings.Default.UserID;
            if (userID == Guid.Empty)
            {
                Properties.UserSettings.Default.UserID = Guid.NewGuid();
                Properties.UserSettings.Default.Save();
            }

            var speechLanguage = Properties.UserSettings.Default.SpeechLanguage;
            if (string.IsNullOrWhiteSpace(speechLanguage))
            {
                Properties.UserSettings.Default.SpeechLanguage = SpeechLanguage.Russian.ToString();
                Properties.UserSettings.Default.Save();
            }
        }
    }
}
