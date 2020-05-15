using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Exercise2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CancellationTokenSource _cancellationTokenSource;
        public MainPage()
        {
            this.InitializeComponent();
            DisableButton(CancelBtn);
        }

        private async void OnStart(object sender, RoutedEventArgs e)
        {
            EnableButton(CancelBtn);
            DisableButton(StartBtn);
            _cancellationTokenSource = new CancellationTokenSource();
            await DoStart(_cancellationTokenSource.Token);
        }

        private void OnCancel(object sender, RoutedEventArgs e) 
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
                EnableButton(StartBtn);
                DisableButton(CancelBtn);
                Progress1.Value = 0;
                Progress2.Value = 0;
                Progress3.Value = 0;
                Progress4.Value = 0;
                Progress5.Value = 0;
            }
        }

        private void DisableButton(Button button)
        {
            button.IsEnabled = false;
        }

        private void EnableButton(Button button)
        {
            button.IsEnabled = true;
        }

        private async Task DoStart(CancellationToken token)
        {
            var tasks = new List<Task>
            {
                FillProgressBar(seconds: 3, Progress1, token),
                FillProgressBar(seconds: 5, Progress2, token),
                FillProgressBar(seconds: 8, Progress3, token),
                FillProgressBar(seconds: 12, Progress4, token),
                FillProgressBar(seconds: 17, Progress5, token)
            };
            await Task.WhenAll(tasks);
            EnableButton(StartBtn);
            DisableButton(CancelBtn);
        }

        private async Task FillProgressBar(int seconds, ProgressBar progressBar, CancellationToken token)
        {
            const int MILISECONDS_PER_SECOND = 1000;
            int delay = (seconds * MILISECONDS_PER_SECOND) / (int)progressBar.Maximum;
            for (int i = 0; i <= progressBar.Maximum; i++)
            {
                if (token.IsCancellationRequested)
                {
                    progressBar.Value = 0;
                    return;
                }
                await Task.Delay(delay);
                await CoreApplication.MainView.CoreWindow.Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        progressBar.Value = i;
                    });
            }
        }
    }
}
