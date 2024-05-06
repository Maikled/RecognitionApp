using CommunityToolkit.Mvvm.ComponentModel;
using RecognitionApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognitionApp.ViewModel
{
    public partial class AudioProcessingViewModel : ObservableObject
    {
        public FileRecognition FileRecognition { get; set; }
    }
}
