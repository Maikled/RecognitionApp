using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using SpeechProcessingClient;

namespace GrpcLibrary.Services
{
    public class SpeechProcessingService
    {
        private GrpcChannel _channel;

        public SpeechProcessingService(string serviceAddress)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            _channel = GrpcChannel.ForAddress(serviceAddress, new GrpcChannelOptions() { HttpHandler = handler});
        }

        public async Task<Response> ProcessingAudioAsync(Stream stream, Guid fileId, string fileName, string fileLanguage)
        {
            var client = new SpeechProcessing.SpeechProcessingClient(_channel);
            
            var requestHeaders = new Metadata
            {
                { "file_id", fileId.ToString() },
                { "file_name", fileName },
                { "file_language", fileLanguage }
            };

            var call = client.ProcessingAudio(requestHeaders);

            byte[] buffer = new byte[1024];
            int bytesRead;
            while((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await call.RequestStream.WriteAsync(new Request() { Data = ByteString.CopyFrom(buffer, 0, bytesRead) });
            }

            await call.RequestStream.CompleteAsync();

            var response = await call.ResponseAsync;
            return response;
        }
    }
}
