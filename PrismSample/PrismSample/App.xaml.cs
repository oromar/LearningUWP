using Prism.Unity.Windows;
using PrismSample.Services;
using PrismSample.Utils;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PrismSample
{
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(PageTokens.WELCOME, null);
            return Task.CompletedTask;
        }
        protected override UIElement CreateShell(Frame rootFrame)
        {
            var appShell = new AppShell();
            appShell.SetFrame(rootFrame);
            return appShell;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            RegisterTypeIfMissing(typeof(ICounterService), typeof(CounterService), true);
        }
    }
}
