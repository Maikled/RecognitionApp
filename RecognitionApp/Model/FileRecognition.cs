using CommunityToolkit.Mvvm.ComponentModel;
using RecognitionApp.Model.Enums;
using System;

namespace RecognitionApp.Model
{
    public class FileRecognition : ObservableObject
    {
        public Guid ID { get; set; }
        public string FileName { get; set; }
        public string FileDisplayName { get; set; }
        public string LocalFilePath { get; set; }
        public FileProcessingState FileProcessingState { get; set; }
        public DateTime FileCreate { get; set; }
        public RecognitionResult RecognitionResult { get; set; }
    }
}