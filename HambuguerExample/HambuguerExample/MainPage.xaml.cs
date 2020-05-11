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

namespace HambuguerExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HamburguerMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;

        }

        private void MenuIcons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuItemSplit1.IsSelected)
            {
                ResultTextBlock.Text = "Menu Item 1";
            }
            else if (MenuItemSplit2.IsSelected)
            {
                ResultTextBlock.Text = "Menu Item 2";
            }
            else if (MenuItemSplit3.IsSelected)
            {
                ResultTextBlock.Text = "Menu Item 3";
            }
            else if (MenuItemSplit4.IsSelected)
            {
                ResultTextBlock.Text = "Menu Item 4";
            }
        }
    }
}
