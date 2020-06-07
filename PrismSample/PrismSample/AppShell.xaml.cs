using Windows.UI.Xaml.Controls;

namespace PrismSample
{
    public sealed partial class AppShell : UserControl
    {
        public AppShell()
        {
            this.InitializeComponent();
        }

        public void SetFrame(Frame frame)
        {
            FrameContainer.Content = frame;
        }
    }
}
