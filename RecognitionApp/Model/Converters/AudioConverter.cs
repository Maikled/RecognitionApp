using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace RecognitionApp.Model.Converters
{
    public class AudioConverter
    {
        public static async Task<StorageFile> ConvertMp3ToWav(StorageFile storageFile)
        {
            var convertedFilePath = Path.Combine(ApplicationData.Current.TemporaryFolder.Path, storageFile.DisplayName + ".wav");

            using (var mp3Reader = new Mp3FileReader(storageFile.Path))
            {
                WaveFileWriter.CreateWaveFile(convertedFilePath, mp3Reader);
            }

            return await StorageFile.GetFileFromPathAsync(convertedFilePath);
        }
    }
}
