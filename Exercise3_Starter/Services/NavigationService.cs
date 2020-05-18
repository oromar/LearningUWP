using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Media.Capture.Frames;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static Exercise3.Views.Page2;

namespace Exercise3.Services
{
    public class NavigationService
    {
        private Frame frame;
        private static NavigationService instance;
        private int mainViewId = 0;
        private CoreApplicationView mainView;
        private readonly IDictionary<int, CoreApplicationView> windows;

        public enum Caller
        {
            Menu,
            MainWindow,
            SecondaryWindow
        }

        private NavigationService()
        {
            windows = new Dictionary<int, CoreApplicationView>();
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequest;
        }

        public static NavigationService Instance
        {
            get
            {
                if (instance == null)
                    instance = new NavigationService();
                return instance;
            }
        }

        public async Task NavigateAsync<T>(object parameter = null) where T : Page
        {
            await mainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (frame.CurrentSourcePageType != typeof(T) || parameter != null)
                {
                    frame.Navigate(typeof(T), parameter);
                }
            });
        }

        public void SetFrame(Frame frame)
        {
            mainView = CoreApplication.GetCurrentView();
            mainViewId = ApplicationView.GetForCurrentView().Id;

            this.frame = frame;
            this.frame.Navigated += OnNavigate;
        }

        public async Task GoBackAsync()
        {
            await mainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (frame.CanGoBack)
                    frame.GoBack();
            });
        }

        public async Task CloseWindowsAsync()
        {
            var list = windows.Values.ToList();
            foreach (var window in list)
            {
                await window.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Window.Current.Close();
                });
            }
            windows.Clear();
        }

        public async Task GoBackToMainViewAsync()
        {
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(mainViewId);
        }

        public async Task CreateNewWindowAsync<T>(object parameter = null) where T : Page
        {
            var newView = CoreApplication.CreateNewView();
            int viewId = 0;
            await newView.Dispatcher
                .RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var newFrame = new Frame();
                newFrame.Navigate(typeof(T), parameter);
                Window.Current.Content = newFrame;
                Window.Current.Closed += OnCloseNewWindow; ;
                Window.Current.Activate();

                viewId = ApplicationView.GetForCurrentView().Id;
                windows.Add(viewId, CoreApplication.GetCurrentView());
            });
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }

        private void OnCloseNewWindow(object sender, CoreWindowEventArgs e)
        {
            var viewId = ApplicationView.GetForCurrentView().Id;
            windows.Remove(viewId);
        }

        private void OnNavigate(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager
                .GetForCurrentView()
                .AppViewBackButtonVisibility = frame.CanGoBack ? 
                                            AppViewBackButtonVisibility.Visible :
                                            AppViewBackButtonVisibility.Collapsed;
        }

        private async void OnBackRequest(object sender, BackRequestedEventArgs e)
        {
            await GoBackAsync();
        }
    }
}
