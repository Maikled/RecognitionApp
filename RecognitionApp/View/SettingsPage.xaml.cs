using Microsoft.UI.Xaml.Controls;
using RecognitionApp.Properties;
using CommunityToolkit.Mvvm.Input;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using RecognitionApp.Model.Enums;

namespace RecognitionApp.View
{
    public sealed partial class SettingsPage : Page
    {
        public string ServerAddress { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<string> AvailableSpeechLanguages { get; set; }
        public SpeechLanguage CurrentSpeechLanguage { get; set; }

        public SettingsPage()
        {
            ServerAddress = UserSettings.Default.ServerAddress;
            UserId = UserSettings.Default.UserID;
            AvailableSpeechLanguages = Enum.GetNames(typeof(SpeechLanguage));

            if(Enum.TryParse<SpeechLanguage>(UserSettings.Default.SpeechLanguage, out var speechLanguage))
            {
                CurrentSpeechLanguage = speechLanguage;
            }

            this.InitializeComponent();
        }

        public void SaveSettings()
        {
            UserSettings.Default.Save();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveSettings();
        }

        [RelayCommand]
        public async Task CheckConnectionServer()
        {
            try
            {
                var pingSender = new Ping();
                var response = await pingSender.SendPingAsync($"http://{ServerAddress}");
                if (response != null)
                {
                    if (response.Status == IPStatus.Success)
                    {
                        connectionFontIcon.Glyph = "\uE930";
                    }
                    else
                    {
                        connectionFontIcon.Glyph = "\uE783";
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
