using System;
using System.Collections.ObjectModel;

namespace RecognitionApp.Model
{
    public class RecognitionResultSpeaker
    {
        public string Speaker { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public ObservableCollection<RecognitionResultSegment> ResultSegments { get; set; } = new ObservableCollection<RecognitionResultSegment>();
    }
}