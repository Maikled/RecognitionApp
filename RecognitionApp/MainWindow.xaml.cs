using GrpcLibrary.Services;
using Microsoft.UI.Xaml;
using RecognitionApp.Model.Converters;
using RecognitionApp.Properties;
using RecognitionApp.View;
using System.IO;

namespace RecognitionApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            var speechProcessingService = new SpeechProcessingService("https://localhost:7000");

            using(var fileStream = new FileStream(@"C:\Users\zyuly\source\repos\RecognitionServer\RecognitionServer\Files\multichannel.wav", FileMode.Open))
            {
                var result = await speechProcessingService.ProcessingAudioAsync(fileStream, UserSettings.Default.UserID, "multichannel.wav", UserSettings.Default.SpeechLanguage);
                var convertedResults = SpeechProcessingConverter.ConvertProcessingResults(result);
            }
        }

        private void MainWindowFrame_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new GeneralPage();
        }
    }
}
