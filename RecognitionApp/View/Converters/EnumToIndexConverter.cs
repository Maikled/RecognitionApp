using Microsoft.UI.Xaml.Data;
using RecognitionApp.Model.Enums;
using System;

namespace RecognitionApp.View.Converters
{
    public class EnumToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is SpeechLanguage speechLanguage)
            {
                return (int)speechLanguage;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if(value is int index)
            {
                return (SpeechLanguage)index;
            }

            return default(SpeechLanguage);
        }
    }
}
