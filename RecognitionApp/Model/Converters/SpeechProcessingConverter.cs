using SpeechProcessingClient;
using System;
using System.Linq;

namespace RecognitionApp.Model.Converters
{
    public class SpeechProcessingConverter
    {
        public static RecognitionResult ConvertProcessingResults(Response results)
        {
            return new RecognitionResult(Guid.Parse(results.Id), results.ResultsData.Select(p =>
                new RecognitionResultSpeaker(p.CurrentSpeaker, p.SegmentsData.Select(s =>
                    new RecognitionResultSegment(s.Text, TimeSpan.FromMilliseconds(s.Start), TimeSpan.FromMilliseconds(s.End))))), results.StatusCode, results.Error);
        }
    }
}