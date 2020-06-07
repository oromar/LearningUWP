using PrismSample.ViewModels;
using Windows.UI.Xaml.Controls;


namespace PrismSample.Views
{
    public sealed partial class MenuView : UserControl
    {
        public MenuViewModel ViewModel => (MenuViewModel)(DataContext);
        public MenuView()
        {
            this.InitializeComponent();
        }
    }
}
