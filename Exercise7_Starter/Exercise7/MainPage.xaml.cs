using BackgroundTask;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Exercise7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string SyncTaskName = "SyncTask";
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var reg in BackgroundTaskRegistration.AllTasks)
            {
                if (reg.Value.Name == SyncTaskName)
                {
                    var registration = reg.Value;
                    registration.Progress += OnProgress;
                    registration.Completed += OnCompleted;
                    SyncBtn.IsEnabled = false;
                    break;
                }
            }
        }

        private void OnStartSync(object sender, RoutedEventArgs e)
        {
            SyncBtn.IsEnabled = false;
            RegisterBackgroundTask();
        }

        private async void RegisterBackgroundTask()
        {
            await BackgroundExecutionManager.RequestAccessAsync();

            var builder = new BackgroundTaskBuilder
            {
                Name = SyncTaskName,
                TaskEntryPoint = "BackgroundTask.SyncBackgroundTask"
            };
            var applicationTrigger = new ApplicationTrigger();
            builder.SetTrigger(applicationTrigger);

            var registration = builder.Register();
            registration.Completed += OnCompleted;
            registration.Progress += OnProgress;

            await applicationTrigger.RequestAsync();
        }

        private async void OnProgress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SyncProgress.Value = args.Progress;
            });
        }

        private async void OnCompleted(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SyncBtn.IsEnabled = true;
            });
        }
    }
}
