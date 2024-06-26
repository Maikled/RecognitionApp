using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using NAudio.Wave;
using RecognitionApp.Model;
using RecognitionApp.Model.Converters;
using RecognitionApp.Model.Enums;
using RecognitionApp.ViewModel;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;


namespace RecognitionApp.View
{
    public sealed partial class AudioRecordingPage : Page
    {
        private AudioRecordingViewModel _viewModel { get; set; } = new AudioRecordingViewModel();
        private GeneralViewModel _generalViewModel { get; set; }
        private WaveIn _waveIn { get; set; }
        private WaveFileWriter _waveFileWriter { get; set; }

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
            if(audioFile.FileType == ".mp3")
            {
                audioFile = await AudioConverter.ConvertMp3ToWav(audioFile);
            }

            AddFileRecognition(audioFile);
        }

        [RelayCommand]
        public void OpenRecordingPanel()
        {
            recordingTeachingTip.IsOpen = !recordingTeachingTip.IsOpen;
        }

        [RelayCommand]
        public void RecordingAudioStart()
        {
            startRecordingButton.IsEnabled = false;
            stopRecordingButton.IsEnabled = true;

            if(_waveIn == null)
            {
                _waveIn = new WaveIn();
                _waveIn.WaveFormat = new WaveFormat(16000, 16, 1);
                _waveIn.DataAvailable += _waveIn_DataAvailable;
            }

            var filePath = Path.Combine(ApplicationData.Current.TemporaryFolder.Path, $"recording-{Guid.NewGuid()}.wav");
            _waveFileWriter = new WaveFileWriter(filePath, _waveIn.WaveFormat);
            
            _waveIn.StartRecording();
        }

        [RelayCommand]
        public async Task RecordingAudioStop()
        {
            _waveIn?.StopRecording();

            if(_waveFileWriter != null)
            {
                _waveIn.Dispose();
                _waveIn = null;
                _waveFileWriter.Close();

                var storageFile = await StorageFile.GetFileFromPathAsync(_waveFileWriter.Filename);
                AddFileRecognition(storageFile);

                _waveFileWriter.Dispose();
                _waveFileWriter = null;
            }

            startRecordingButton.IsEnabled = true;
            stopRecordingButton.IsEnabled = false;
        }

        private void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
            recordingTimeTextBlock.Text = _waveFileWriter.TotalTime.ToString(@"hh\:mm\:ss");
        }

        private async void AddFileRecognition(StorageFile storageFile)
        {
            if(_generalViewModel != null && storageFile != null)
            {
                var fileId = Guid.NewGuid();

                var localFolder = ApplicationData.Current.LocalFolder;
                var saveStorageFileFolder = await localFolder.CreateFolderAsync(fileId.ToString());

                var savedStorageFile = await FileManager.SaveAudioFileAsync(storageFile, saveStorageFileFolder);

                await savedStorageFile.RenameAsync(fileId.ToString() + savedStorageFile.FileType);

                var fileRecognition = new FileRecognition();
                fileRecognition.ID = fileId;
                fileRecognition.FileName = savedStorageFile.Name;
                fileRecognition.FileDisplayName = Path.GetFileNameWithoutExtension(storageFile.DisplayName);
                fileRecognition.LocalFilePath = savedStorageFile.Path;
                fileRecognition.FileProcessingState = FileProcessingState.Created;
                fileRecognition.FileCreate = DateTime.Now;

                await FileManager.SaveObjectJsonAsync(fileRecognition, saveStorageFileFolder);

                _generalViewModel.RecognitionResults.Add(fileRecognition);
            }
        }
    }
}
