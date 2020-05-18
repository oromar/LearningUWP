using Exercise3.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Devices.AllJoyn;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input.Spatial;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static Exercise3.Services.NavigationService;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Exercise3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Caller caller)
            {
                switch (caller)
                {
                    case Caller.Menu:
                        OpenWindowBtn.Visibility = Visibility.Visible;
                        break;
                    case Caller.MainWindow:
                        MainWindowBtn.Visibility = Visibility.Visible;
                        break;
                    case Caller.SecondaryWindow:
                        BackToMainTxt.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private async void OpenWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            await NavigationService.Instance.CreateNewWindowAsync<Page2>(Caller.MainWindow);
        }

        private async void MainWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            await NavigationService.Instance.NavigateAsync<Page2>(Caller.SecondaryWindow);
            await NavigationService.Instance.GoBackToMainViewAsync();
            Window.Current.Close();
        }
    }
}
