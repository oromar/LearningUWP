using PrismSample.ViewModels;
using Windows.UI.Xaml.Controls;

namespace PrismSample.Views
{
    public sealed partial class WelcomePage : Page
    {
        public WelcomePageViewModel ViewModel => (WelcomePageViewModel)(DataContext);

        public WelcomePage()
        {
            this.InitializeComponent();
        }
    }
}
