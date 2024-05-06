using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using RecognitionApp.Model;
using RecognitionApp.ViewModel;

namespace RecognitionApp.View
{
    public sealed partial class AudioProcessingPage : Page
    {
        private AudioProcessingViewModel _viewModel { get; set; } = new AudioProcessingViewModel(); 

        public AudioProcessingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is FileRecognition fileRecognition)
            {
                _viewModel.FileRecognition = fileRecognition;
            }
        }
    }
}
