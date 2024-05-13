using CommunityToolkit.Mvvm.ComponentModel;
using GrpcLibrary.Services;
using RecognitionApp.Model;
using RecognitionApp.Model.Converters;
using RecognitionApp.Model.Enums;
using RecognitionApp.Properties;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace RecognitionApp.ViewModel
{
    public partial class AudioProcessingViewModel : ObservableObject
    {
        public FileRecognition FileRecognition { get; set; }

        public event EventHandler<FileProcessingState> FileProcessingStateChanged;

        public async Task RunningSpeechProcess()
        {
            if(FileRecognition != null)
            {
                FileProcessingStateChanged?.Invoke(FileRecognition, FileRecognition.FileProcessingState);

                if(FileRecognition.FileProcessingState != FileProcessingState.Transcribed)
                {
                    try
                    {
                        FileProcessingStateChanged?.Invoke(FileRecognition, FileProcessingState.Transcribing);

                        var speechProcessingService = new SpeechProcessingService("https://localhost:7000");
                        SpeechProcessingClient.Response result;

                        var file = await StorageFile.GetFileFromPathAsync(FileRecognition.LocalFilePath);
                        using (var fileStream = await file.OpenStreamForReadAsync())
                        {
                            result = await speechProcessingService.ProcessingAudioAsync(fileStream, UserSettings.Default.UserID, FileRecognition.FileName, UserSettings.Default.SpeechLanguage);
                        }

                        if (result != null && result.StatusCode == 200)
                        {
                            FileRecognition.RecognitionResult = SpeechProcessingConverter.ConvertProcessingResults(result);
                            FileRecognition.FileProcessingState = FileProcessingState.Transcribed;

                            var saveFileFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync(FileRecognition.ID.ToString());
                            await FileManager.SaveObjectJsonAsync(FileRecognition, saveFileFolder);

                            FileProcessingStateChanged?.Invoke(FileRecognition, FileProcessingState.Transcribed);
                        }
                        else
                        {
                            throw new Exception(result.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        FileProcessingStateChanged?.Invoke(FileRecognition, FileProcessingState.Failed);
                    }
                }
            }
        }
    }
}