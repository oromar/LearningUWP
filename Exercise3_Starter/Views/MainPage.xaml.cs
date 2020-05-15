using Exercise3.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static Exercise3.Views.Page2;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Exercise3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavigationService.Instance.SetFrame(NavigationFrame);
            NavigationService.Instance.NavigateAsync<Page1>();
        }

        private async void HandleButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Name)
            {
                case nameof(Page1Button):
                    await NavigationService.Instance.NavigateAsync<Page1>();
                    break;
                case nameof(Page2Button):
                    await NavigationService.Instance.NavigateAsync<Page2>(State.Menu);
                    break;
                case nameof(Page3Button):
                    await NavigationService.Instance.NavigateAsync<Page3>();
                    break;
                case nameof(CloseWindowsButton):
                    await NavigationService.Instance.CloseWindowsAsync();
                    return;
            }

        }

        private async void CurrentView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            await NavigationService.Instance.GoBackAsync();
        }
    }
}

