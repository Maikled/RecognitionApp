using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace RecognitionApp.ViewModel
{
    public class AudioRecordingViewModel
    {
        public IList<string> FileTypes = new string[2] { ".mp3", ".wav" };

        public async Task<StorageFile> SelectAudioFileAsync()
        {
            var filePicker = new FileOpenPicker();

            var hWnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(filePicker, hWnd);

            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            
            foreach (var type in FileTypes)
            {
                filePicker.FileTypeFilter.Add(type);
            }

            return await filePicker.PickSingleFileAsync();
        }
    }
}