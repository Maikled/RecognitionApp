using System;

namespace RecognitionApp.Model
{
    public class RecognitionResultSegment
    {
        public string Text { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public TimeSpan Duration { get { return End - Start; } }

        public RecognitionResultSegment(string text, TimeSpan start, TimeSpan end)
        {
            Text = text;
            Start = start;
            End = end;
        }
    }
}
