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
                var format = new WaveFormat(32000, 16, 1);
                using(var stream = new WaveFormatConversionStream(format, mp3Reader))
                {
                    WaveFileWriter.CreateWaveFile(convertedFilePath, stream);
                }
            }

            return await StorageFile.GetFileFromPathAsync(convertedFilePath);
        }
    }
}
