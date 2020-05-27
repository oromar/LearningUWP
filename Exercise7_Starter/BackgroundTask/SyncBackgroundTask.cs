using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BackgroundTask
{
    public sealed class SyncBackgroundTask : IBackgroundTask
    {
        BackgroundTaskDeferral _Deferral;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _Deferral = taskInstance.GetDeferral();

            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(200);
                taskInstance.Progress += 1;
            }

            _Deferral.Complete();

            taskInstance.Task.Unregister(false);
        }
    }
}
