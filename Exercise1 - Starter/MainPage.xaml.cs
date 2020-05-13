using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Effects;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Exercise1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private readonly BitmapImage image1;
        private readonly BitmapImage image2;
        private readonly BitmapImage image3;
        private readonly BitmapImage image4;

        public MainPage()
        {
            this.InitializeComponent();
            image1 = new BitmapImage(new Uri(base.BaseUri, @"Assets/Images/image1.jpg"));
            image2 = new BitmapImage(new Uri(base.BaseUri, @"Assets/Images/image2.jpg"));
            image3 = new BitmapImage(new Uri(base.BaseUri, @"Assets/Images/image3.jpg"));
            image4 = new BitmapImage(new Uri(base.BaseUri, @"Assets/Images/image4.jpg"));

        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Name)
            {
                case nameof(ImageButton1):
                    ImageLoader.Source = image1;
                    break;
                case nameof(ImageButton2):
                    ImageLoader.Source = image2;
                    break;
                case nameof(ImageButton3):
                    ImageLoader.Source = image3;
                    break;
                case nameof(ImageButton4):
                    ImageLoader.Source = image4;
                    break;
            }
        }

        private void SyncToggleButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (SyncToggleButton.IsChecked.HasValue && SyncToggleButton.IsChecked.Value)
            {
                SyncPanel.Visibility = Visibility.Visible;
                StartSync.Begin();
            }
            else
            {
                SyncPanel.Visibility = Visibility.Collapsed;
                StartSync.Stop();
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }
    }
}
