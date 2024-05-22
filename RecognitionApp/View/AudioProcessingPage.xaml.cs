using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Navigation;
using RecognitionApp.Model;
using RecognitionApp.Model.Enums;
using RecognitionApp.ViewModel;
using System;
using System.Linq;
using Windows.Media.Core;

namespace RecognitionApp.View
{
    public sealed partial class AudioProcessingPage : Page
    {
        private AudioProcessingViewModel _viewModel { get; set; } = new AudioProcessingViewModel(); 

        public AudioProcessingPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is FileRecognition fileRecognition)
            {
                _viewModel.FileRecognition = fileRecognition;
                _viewModel.FileProcessingStateChanged += _viewModel_FileProcessingStateChanged;

                SetMediaSource(fileRecognition);
                await _viewModel.RunningSpeechProcess();

                if(fileRecognition.RecognitionResult != null)
                {
                    recognitionResultsItemsControl.ItemsSource = fileRecognition.RecognitionResult.RecognitionResults;
                }
            }
        }

        private void _viewModel_FileProcessingStateChanged(object sender, Model.Enums.FileProcessingState e)
        {
            switch(e)
            {
                default:
                case FileProcessingState.Created:
                case FileProcessingState.Uploaded:
                case FileProcessingState.Converting:
                    statusContentControl.Content = (this.Resources["convertingInfoBarDataTemplate"] as DataTemplate).LoadContent();
                    break;
                case FileProcessingState.Transcribing:
                    statusContentControl.Content = (this.Resources["transcribingInfoBarDataTemplate"] as DataTemplate).LoadContent();
                    break;
                case FileProcessingState.Transcribed:
                    statusContentControl.Content = (this.Resources["transcribedInfoBarDataTemplate"] as DataTemplate).LoadContent();
                    break;
                case FileProcessingState.Failed:
                    statusContentControl.Content = (this.Resources["failedInfoBarDataTemplate"] as DataTemplate).LoadContent();
                    break;
            }
        }

        private void SetMediaSource(FileRecognition fileRecognition)
        {
            var mediaSource = MediaSource.CreateFromUri(new Uri(fileRecognition.LocalFilePath));
            audioPlayer.Source = mediaSource;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as StackPanel).Translation += new System.Numerics.Vector3(0, 0, 16);
        }

        private void RichTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            var richTextBlock = sender as RichTextBlock;
            richTextBlock.Blocks.Clear();

            

            var dataContext = (richTextBlock.DataContext as RecognitionResultSpeaker);
            foreach(var item in dataContext.ResultSegments)
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run() { Text = item.Text });
                richTextBlock.Blocks.Add(paragraph);
            }
        }
    }
}
