using SpeechProcessingClient;
using System;
using System.Collections.Generic;
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
                    ResultSegments = new ObservableCollection<RecognitionResultSegment>(p.SegmentsData.Select(s =>
                    new RecognitionResultSegment(s.Text, TimeSpan.FromMilliseconds(s.Start), TimeSpan.FromMilliseconds(s.End))))
                })),
            };

            recognitionResult.DisplayRecognitionResults = ConvertDisplayRecognitionResult(recognitionResult.RecognitionResults);

            return recognitionResult;
        }

        public static ObservableCollection<DisplayRecognitionResultSpeaker> ConvertDisplayRecognitionResult(IEnumerable<RecognitionResultSpeaker> recognitionResult)
        {
            var result = new ObservableCollection<DisplayRecognitionResultSpeaker>();
            
            foreach(var item in recognitionResult)
            {
                var speakerResult = new DisplayRecognitionResultSpeaker();
                speakerResult.CurrentSpeaker = item.Speaker;
                speakerResult.Start = item.ResultSegments.Min(s => s.Start).ToString("hh\\:mm\\:ss");
                speakerResult.End = item.ResultSegments.Max(s => s.End).ToString("hh\\:mm\\:ss");
                speakerResult.Text = string.Join("\n", item.ResultSegments.Select(s => s.Text));
                result.Add(speakerResult);
            }

            return result;
        }
    }
}