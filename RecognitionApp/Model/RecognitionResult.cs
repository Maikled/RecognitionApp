using System;
using System.Collections.Generic;

namespace RecognitionApp.Model
{
    public class RecognitionResult
    {
        public Guid ID { get; }
        public IEnumerable<RecognitionResultSpeaker> RecognitionResults { get; }
        public int StatusCode { get; }
        public string ErrorMessage { get; }

        public RecognitionResult(Guid id, IEnumerable<RecognitionResultSpeaker> recognitionResults, int statusCode, string errorMessage)
        {
            ID = id;
            RecognitionResults = recognitionResults;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
