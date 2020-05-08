using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Caliburn.Micro;

using DemoUWP.Core.Models;
using DemoUWP.Core.Services;
using DemoUWP.Helpers;

namespace DemoUWP.ViewModels
{
    public class DataGridViewModel : Screen
    {
        public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public DataGridViewModel()
        {
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // TODO WTS: Replace this with your actual data
            var data = await SampleDataService.GetGridDataAsync();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
    }
}
