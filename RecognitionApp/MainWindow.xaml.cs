using Microsoft.UI.Xaml;
using RecognitionApp.View;

namespace RecognitionApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            SetTitleBar();
            SetSystemBackdrop();

            this.InitializeComponent();
        }

        private void MainWindowFrame_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new GeneralPage();
        }

        private void SetTitleBar()
        {
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(appTitleBar);
        }

        private void SetSystemBackdrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
            {
                Microsoft.UI.Xaml.Media.MicaBackdrop micaBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                micaBackdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;
                this.SystemBackdrop = micaBackdrop;
            }
            else
            {
                if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
                {
                    Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop DesktopAcrylicBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
                    this.SystemBackdrop = DesktopAcrylicBackdrop;
                }
            }
        }
    }
}
