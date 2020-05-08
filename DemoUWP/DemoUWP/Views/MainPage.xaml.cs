using System;

using DemoUWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace DemoUWP.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }
    }
}
