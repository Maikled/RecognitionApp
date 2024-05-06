using System.Collections.Generic;

namespace RecognitionApp.Model
{
    public class RecognitionResultSpeaker
    {
        public string Speaker { get; set; }
        public IEnumerable<RecognitionResultSegment> ResultSegments { get; set; } = new List<RecognitionResultSegment>();

        public RecognitionResultSpeaker(string speaker)
        {
            Speaker = speaker;
        }

        public RecognitionResultSpeaker(string speaker, IEnumerable<RecognitionResultSegment> resultSegments) : this(speaker)
        {
            ResultSegments = resultSegments;
        }
    }
}