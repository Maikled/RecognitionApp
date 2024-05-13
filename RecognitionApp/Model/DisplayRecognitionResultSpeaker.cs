using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace RecognitionApp.Model
{
    public partial class DisplayRecognitionResultSpeaker : ObservableObject
    {
        public string CurrentSpeaker { get; set; }
        public string Start {  get; set; }
        public string End { get; set; }
        public string Text { get; set; }
    }
}