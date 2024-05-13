using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RecognitionApp.Model;
using RecognitionApp.ViewModel;
using System;

namespace RecognitionApp.View
{
    public sealed partial class GeneralPage : Page
    {
        private GeneralViewModel _viewModel { get; set; } = new GeneralViewModel(); 

        public GeneralPage()
        {
            _viewModel.RecognitionResults.CollectionChanged += RecognitionResults_CollectionChanged;
            this.Loaded += GeneralPage_Loaded;

            this.InitializeComponent();
        }

        private async void GeneralPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await _viewModel.LoadLocalFileRecognitionsAsync();
        }

        private void RecognitionResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is FileRecognition fileRecognition)
                    {
                        var newNavigationViewItem = new NavigationViewItem();
                        newNavigationViewItem.Content = fileRecognition.FileDisplayName;
                        newNavigationViewItem.Icon = new SymbolIcon(Symbol.OpenFile);
                        newNavigationViewItem.Tag = item;
                        GeneralNavigationView.MenuItems.Add(newNavigationViewItem);
                    }
                }
            }
            
            if(e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {

                }
            }
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if(args.IsSettingsInvoked)
            {

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
    }
}
