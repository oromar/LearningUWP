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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StupendousStylesChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoffeePage : Page
    {
        public CoffeePage()
        {
            this.InitializeComponent();
        }

        private void HandleCoffe_Click(object sender, RoutedEventArgs e)
        {
            var option = sender as MenuFlyoutItem;
            var result = string.Empty;
            if (option.Text == "None")
            {
                CoffeeResultText.Text = string.Empty;
            }
            else
            {
                if (string.IsNullOrEmpty(CoffeeResultText.Text))
                {
                    result = option.Text;
                }
                else
                {
                    result = CoffeeResultText.Text + " + " + option.Text;
                }
                CoffeeResultText.Text = result;
            }
        }
    }
}
