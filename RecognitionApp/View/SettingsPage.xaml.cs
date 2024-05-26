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

        [RelayCommand]
        public async Task CheckConnectionServer()
        {
            try
            {
                var pingSender = new Ping();
                var uri = new Uri($"https://{ServerAddress}");
                var address = uri.Scheme + "://" + uri.Host;

                var response = await pingSender.SendPingAsync(address);
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

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var text = (sender as TextBox).Text;
            if(!string.IsNullOrWhiteSpace(text))
            {
                UserSettings.Default.ServerAddress = text;
                UserSettings.Default.Save();
            }
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            var text = (sender as TextBox).Text;
            if(Guid.TryParse(text, out var id))
            {
                UserSettings.Default.UserID = id;
                UserSettings.Default.Save();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var language = (sender as ComboBox).SelectedItem;
            if(Enum.TryParse(typeof(SpeechLanguage), language.ToString(), out var result))
            {
                UserSettings.Default.SpeechLanguage = result.ToString();
                UserSettings.Default.Save();
            }
        }
    }
}
