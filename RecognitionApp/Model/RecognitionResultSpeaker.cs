using System.Collections.ObjectModel;

namespace RecognitionApp.Model
{
    public class RecognitionResultSpeaker
    {
        public string Speaker { get; set; }
        public ObservableCollection<RecognitionResultSegment> ResultSegments { get; set; } = new ObservableCollection<RecognitionResultSegment>();
    }
}