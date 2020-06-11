using MarvelHeroesExplorer.ViewModels;
using Windows.UI.Xaml.Controls;


namespace MarvelHeroesExplorer.Views
{
    public sealed partial class HeroesPage : Page
    {
        public HeroesPageViewModel ViewModel => (HeroesPageViewModel)(DataContext);
        
        public HeroesPage()
        {
            this.InitializeComponent();
        }
    }
}
