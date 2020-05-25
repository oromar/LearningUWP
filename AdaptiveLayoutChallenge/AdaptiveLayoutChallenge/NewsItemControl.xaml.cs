using AdaptiveLayoutChallenge.Models;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace AdaptiveLayoutChallenge
{
    public sealed partial class NewsItemControl : UserControl
    {

        public NewsItem NewsItem { get { return DataContext as NewsItem; } }
        public NewsItemControl()
        {
            this.InitializeComponent();
            DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
