using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using PrismSample.Events;
using PrismSample.Services;
using System.Collections.Generic;

namespace PrismSample.ViewModels
{
    public class StatusPageViewModel : ViewModelBase
    {
        public StatusPageViewModel(ICounterService counterService, IEventAggregator eventAggregator, INavigationService navigationService)
        {
            Counter = counterService.GetCount();
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;
        }

        private string status = "None";
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        private int counter;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;

        public int Counter
        {
            get { return counter; }
            set { SetProperty(ref counter, value); }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            Status = e.Parameter as string;
            var authEvent = eventAggregator.GetEvent<AuthChangeEvent>();
            if (!authEvent.Contains(OnAuthChange))
                authEvent.Subscribe(OnAuthChange);
            base.OnNavigatedTo(e, viewModelState);
        }

        private void OnAuthChange(bool logged)
        {
            if (!logged && navigationService.CanGoBack())
                navigationService.GoBack();
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            var authEvent = eventAggregator.GetEvent<AuthChangeEvent>();
            if (authEvent.Contains(OnAuthChange))
                authEvent.Unsubscribe(OnAuthChange);
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }
    }
}
