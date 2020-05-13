using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StupendousStylesChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavigationFrame.Navigate(typeof(DonutPage));
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            var to = sender as Button;
            switch (to.Name)
            {
                case nameof(DonutButton):
                    NavigationFrame.Navigate(typeof(DonutPage));
                    break;
                case nameof(CoffeeButton):
                    NavigationFrame.Navigate(typeof(CoffeePage));
                    break;
                case nameof(ScheduleButton):
                    NavigationFrame.Navigate(typeof(SchedulePage));
                    break;
                case nameof(CompleteButton):
                    NavigationFrame.Navigate(typeof(CompletePage));
                    break;
            }
        }
    }
}
