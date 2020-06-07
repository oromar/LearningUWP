using PrismSample.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PrismSample.Views
{
    public sealed partial class StatusPage : Page
    {
        public StatusPageViewModel ViewModel => (StatusPageViewModel)(DataContext);
        public StatusPage()
        {
            this.InitializeComponent();
        }
    }
}
