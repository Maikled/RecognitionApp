using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace RecognitionApp.Model
{
    public class RecognitionResult : ObservableObject
    {
        public Guid ID { get; set; }
        public ObservableCollection<RecognitionResultSpeaker> RecognitionResults { get; set; }
        //public ObservableCollection<DisplayRecognitionResultSpeaker> DisplayRecognitionResults { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
