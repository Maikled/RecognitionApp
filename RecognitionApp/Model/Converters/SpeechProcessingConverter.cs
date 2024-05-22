using SpeechProcessingClient;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RecognitionApp.Model.Converters
{
    public class SpeechProcessingConverter
    {
        public static RecognitionResult ConvertProcessingResults(Response results)
        {
            var recognitionResult = new RecognitionResult()
            {
                ID = Guid.Parse(results.Id),
                ErrorMessage = results.Error,
                StatusCode = results.StatusCode,
                RecognitionResults = new ObservableCollection<RecognitionResultSpeaker>(results.ResultsData.Select(p =>
                new RecognitionResultSpeaker()
                {
                    Speaker = p.CurrentSpeaker,
                    Start = TimeSpan.FromMilliseconds(p.SegmentsData.Min(s => s.Start)),
                    End = TimeSpan.FromMilliseconds(p.SegmentsData.Max(s => s.End)),
                    ResultSegments = new ObservableCollection<RecognitionResultSegment>(p.SegmentsData.Select(s =>
                    new RecognitionResultSegment(s.Text.Trim(), TimeSpan.FromMilliseconds(s.Start), TimeSpan.FromMilliseconds(s.End))))
                })),
            };

            return recognitionResult;
        }
    }
}