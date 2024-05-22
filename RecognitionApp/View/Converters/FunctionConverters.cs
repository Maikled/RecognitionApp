using System;

namespace RecognitionApp.View.Converters
{
    public static class FunctionConverters
    {
        public static string ConvertTimeSpanToString(TimeSpan input)
        {
            return input.ToString("hh\\:mm\\:ss");
        }
    }
}