using MarvelHeroesExplorer.Services;
using Prism.Unity.Windows;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace MarvelHeroesExplorer
{
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs e)
        {
            NavigationService.Navigate("Heroes", null);
            return Task.CompletedTask;
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            RegisterTypeIfMissing(typeof(IMarvelApiService), typeof(MarvelApiService), true);
            RegisterTypeIfMissing(typeof(ICacheService), typeof(CacheService), true);
        }
    }
}
