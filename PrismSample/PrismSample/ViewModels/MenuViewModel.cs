using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using PrismSample.Events;
using PrismSample.Utils;

namespace PrismSample.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;

        public MenuViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
        }
        public void NavigateWelcome()
        {
            navigationService.Navigate(PageTokens.WELCOME, "Welcome");
        }
        public void NavigateFail()
        {
            navigationService.Navigate(PageTokens.STATUS, "Fail");
        }

        public void NavigateSuccess()
        {
            navigationService.Navigate(PageTokens.STATUS, "Success");
        }

        public void Login()
        {
            eventAggregator.GetEvent<AuthChangeEvent>().Publish(true);
        }

        public void Logout()
        {
            eventAggregator.GetEvent<AuthChangeEvent>().Publish(false);
        }
    }
}
