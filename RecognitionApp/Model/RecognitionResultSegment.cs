using System;

namespace RecognitionApp.Model
{
    public class RecognitionResultSegment
    {
        public string Text { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpan Duration { get; set; }

        public RecognitionResultSegment() { }

        public RecognitionResultSegment(string text, TimeSpan start, TimeSpan end) : this()
        {
            Text = text;
            Start = start;
            End = end;
            Duration = End - Start;
        }
    }
}
