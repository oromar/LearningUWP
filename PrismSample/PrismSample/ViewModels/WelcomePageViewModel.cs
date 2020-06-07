using Prism.Commands;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using PrismSample.Events;
using PrismSample.Services;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace PrismSample.ViewModels
{
    public class WelcomePageViewModel : ViewModelBase
    {
        public WelcomePageViewModel(ICounterService counterService, IEventAggregator eventAggregator)
        {
            WelcomeText = "Welcome Guy";
            AddCountCommand = new DelegateCommand(Increment, CanExecuteAddCommand).ObservesProperty(() => Amount);
            this.counterService = counterService;
            this.eventAggregator = eventAggregator;
            this.Count = counterService.GetCount();
        }
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            var authChangeEvent = eventAggregator.GetEvent<AuthChangeEvent>();
            if (!authChangeEvent.Contains(OnAuthChange))
                authChangeEvent.Subscribe(OnAuthChange);
        }

        private void OnAuthChange(bool payload)
        {
            LoggedIn = payload;
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            var authChangeEvent = eventAggregator.GetEvent<AuthChangeEvent>();
            if (authChangeEvent.Contains(OnAuthChange))
                authChangeEvent.Unsubscribe(OnAuthChange);
        }

        private string welcomeText;
        public string WelcomeText
        {
            get { return welcomeText; }
            set { SetProperty(ref welcomeText, value); }
        }

        private int count;
        public int Count
        {
            get { return counterService.GetCount(); }
            set { SetProperty(ref count, value); }
        }

        private string amount;
        private readonly ICounterService counterService;
        private readonly IEventAggregator eventAggregator;

        public string Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }

        private bool loggedIn;
        public bool LoggedIn
        {
            get { return loggedIn; }
            set { SetProperty(ref loggedIn, value); }
        }

        public DelegateCommand AddCountCommand { get; set; }

        public void IncrementBy1(object sender, RoutedEventArgs e)
        {
            this.Count = counterService.IncrementCount(1);
        }

        public void Increment()
        {
            this.Count = counterService.IncrementCount(int.Parse(Amount));
        }

        public bool CanExecuteAddCommand()
        {
            return int.TryParse(Amount, out int _);
        }
    }
}
