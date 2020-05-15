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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Exercise3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        private static int _mainWindowViewId = 0;

        public enum State
        {
            Menu,
            MainWindow,
            SecondaryWindow
        }


        public Page2()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is State state)
            {
                switch (state)
                {
                    case State.Menu:
                        OpenWindowBtn.Visibility = Visibility.Visible;
                        break;
                    case State.MainWindow:
                        MainWindowBtn.Visibility = Visibility.Visible;
                        break;
                    case State.SecondaryWindow:
                        BackToMainTxt.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private async void OpenWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            await NavigationService.Instance.CreateNewWindowAsync<Page2>();
        }

        private void Current_Closed(object sender, Windows.UI.Core.CoreWindowEventArgs e)
        {
            var viewId = ApplicationView.GetForCurrentView().Id;
        }

        private async void MainWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            await NavigationService.Instance.GoBackToMainViewAsync<Page2>();
        }
    }
}
