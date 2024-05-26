using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RecognitionApp.Model;
using RecognitionApp.ViewModel;
using System;
using System.Threading.Tasks;

namespace RecognitionApp.View
{
    public sealed partial class GeneralPage : Page
    {
        private GeneralViewModel _viewModel { get; set; } = new GeneralViewModel(); 

        public GeneralPage()
        {
            this.Loaded += GeneralPage_Loaded;
            this.InitializeComponent();
        }

        private async void GeneralPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await _viewModel.LoadLocalFileRecognitionsAsync();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if(args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else
            {
                var tag = args.InvokedItemContainer.Tag;
                var pageTag = tag?.ToString();
                if (args.InvokedItemContainer != null && !string.IsNullOrWhiteSpace(pageTag))
                {
                    var type = Type.GetType(pageTag);
                    var previusePageType = ContentFrame.CurrentSourcePageType;

                    if(type is not null && !Type.Equals(previusePageType, type))
                    {
                        if(tag is FileRecognition)
                        {
                            ContentFrame.Navigate(typeof(AudioProcessingPage), tag, args.RecommendedNavigationTransitionInfo);
                        }
                        else
                        {
                            if(Type.Equals(type, typeof(AudioRecordingPage)))
                            {
                                ContentFrame.Navigate(typeof(AudioRecordingPage), _viewModel);
                            }
                            else
                            {
                                ContentFrame.Navigate(type, null, args.RecommendedNavigationTransitionInfo);
                            }
                        }
                    }
                }
            }
        }

        private void NavigationView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(AudioRecordingPage), _viewModel);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            grid.PointerPressed += (sender, e) =>
            {
                ContentFrame.Navigate(typeof(AudioProcessingPage), grid.DataContext);
            };
        }

        private async void DeleteRecognitionResult_Click(object sender, RoutedEventArgs e)
        {
            var fileRecognition = (sender as FrameworkElement).DataContext as FileRecognition;
            await _viewModel.DeleteRecognitionResult(fileRecognition);
        }
    }
}
