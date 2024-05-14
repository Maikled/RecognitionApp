using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using RecognitionApp.Model;
using RecognitionApp.Model.Enums;
using RecognitionApp.ViewModel;
using System;
using System.Threading.Tasks;
using Windows.Storage;


namespace RecognitionApp.View
{
    public sealed partial class AudioRecordingPage : Page
    {
        private AudioRecordingViewModel _viewModel { get; set; } = new AudioRecordingViewModel();
        private GeneralViewModel _generalViewModel { get; set; }

        public AudioRecordingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is GeneralViewModel generalViewModel)
            {
                _generalViewModel = generalViewModel;
            }
        }

        [RelayCommand]
        public async Task OpenAudioFile()
        {
            var audioFile = await _viewModel.SelectAudioFileAsync();
            AddFileRecognition(audioFile);
        }

        [RelayCommand]
        public void OpenRecordingPanel()
        {
            recordingTeachingTip.IsOpen = !recordingTeachingTip.IsOpen;
        }

        [RelayCommand]
        public async Task RecordingAudioFile()
        {
            
        }

        private async void AddFileRecognition(StorageFile storageFile)
        {
            if(_generalViewModel != null && storageFile != null)
            {
                var fileId = Guid.NewGuid();

                var localFolder = ApplicationData.Current.LocalFolder;
                var saveStorageFileFolder = await localFolder.CreateFolderAsync(fileId.ToString());

                var savedStorageFile = await FileManager.SaveAudioFileAsync(storageFile, saveStorageFileFolder);

                var fileRecognition = new FileRecognition();
                fileRecognition.ID = fileId;
                fileRecognition.FileName = savedStorageFile.Name;
                fileRecognition.FileDisplayName = savedStorageFile.DisplayName;
                fileRecognition.LocalFilePath = savedStorageFile.Path;
                fileRecognition.FileProcessingState = FileProcessingState.Created;
                fileRecognition.FileCreate = DateTime.Now;

                await FileManager.SaveObjectJsonAsync(fileRecognition, saveStorageFileFolder);

                _generalViewModel.RecognitionResults.Add(fileRecognition);
            }
        }
    }
}
