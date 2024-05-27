using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace RecognitionApp.Model
{
    public class FileManager
    {
        public static async Task<StorageFile> SaveAudioFileAsync(StorageFile storageFile, StorageFolder fileDestinationFolder)
        {
            return await storageFile.CopyAsync(fileDestinationFolder, storageFile.Name, NameCollisionOption.GenerateUniqueName);
        }

        public static async Task SaveObjectJsonAsync<T>(T obj, StorageFolder destinationFolder)
        {
            var jsonObject = JsonSerializer.Serialize(obj, GetSerializeOptions());
            var savedFile = await destinationFolder.CreateFileAsync($"{obj.GetType().Name}.json", CreationCollisionOption.ReplaceExisting);
            using (var fileStream = await savedFile.OpenStreamForWriteAsync())
            {
                await fileStream.WriteAsync(Encoding.UTF8.GetBytes(jsonObject));
            }
        }

        public static async Task<T> LoadObjectJsonAsync<T>(StorageFolder destinationFolder)
        {
            var loadFile = await destinationFolder.GetFileAsync($"{typeof(T).Name}.json");
            using (var fileStream = await loadFile.OpenStreamForReadAsync())
            {
                var bytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(bytes);
                return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
            }
        }

        public static async Task<IEnumerable<StorageFolder>> GetFoldersFileAsync(StorageFolder destinationFolder)
        {
            return await destinationFolder.GetFoldersAsync();
        }

        private static JsonSerializerOptions GetSerializeOptions()
        {
            return new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true };
        }
    }
}
