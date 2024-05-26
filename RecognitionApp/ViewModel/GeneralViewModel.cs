using CommunityToolkit.Mvvm.ComponentModel;
using RecognitionApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;

namespace RecognitionApp.ViewModel
{
    public partial class GeneralViewModel : ObservableObject
    {
        public ObservableCollection<FileRecognition> RecognitionResults { get; set; } = new ObservableCollection<FileRecognition>();

        public async Task LoadLocalFileRecognitionsAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var fileFolders = await FileManager.GetFoldersFileAsync(localFolder);
            foreach (var folder in fileFolders)
            {
                var fileRecognition = await FileManager.LoadObjectJsonAsync<FileRecognition>(folder);
                RecognitionResults.Add(fileRecognition);
            }
        }

        public async Task DeleteRecognitionResult(FileRecognition fileRecognition)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var recognitionResultFolder = await localFolder.GetFolderAsync(fileRecognition.ID.ToString());
            if(recognitionResultFolder != null)
            {
                await recognitionResultFolder.DeleteAsync();
            }

            if(RecognitionResults.Contains(fileRecognition))
            {
                RecognitionResults.Remove(fileRecognition);
            }
        }
    }
}