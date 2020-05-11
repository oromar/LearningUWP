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

namespace HamburguerHeavenChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (MyFrame.Content == null)
            {
                MyFrame.Navigate(typeof(Financial));
                TitleTextBlock.Text = "Financial";
            }
        }

        private void HamburguerButton_Click(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private void MenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var selected = listBox.SelectedItem as ListBoxItem;
            if (selected.Name == "FinancialListBoxItem")
            {
                MyFrame.Navigate(typeof(Financial));
                TitleTextBlock.Text = "Financial";
                TitleTextBlock.Margin = new Thickness(60,0,0,0);
                BackButton.Visibility = Visibility.Collapsed;
            }
            else if (selected.Name == "FoodListBoxItem")
            {
                MyFrame.Navigate(typeof(Food));
                TitleTextBlock.Text = "Food";
                TitleTextBlock.Margin = new Thickness(120,0,0,0);
                BackButton.Visibility = Visibility.Visible;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
                TitleTextBlock.Text = "Financial";
                BackButton.Visibility =Visibility.Collapsed;
                TitleTextBlock.Margin = new Thickness(60,0,0,0);
                MenuListBox.SelectedValue = FinancialListBoxItem;
            }
        }
    }
}
