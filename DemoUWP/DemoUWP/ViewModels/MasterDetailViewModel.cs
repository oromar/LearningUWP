using System;
using System.Linq;
using System.Threading.Tasks;

using Caliburn.Micro;

using DemoUWP.Core.Services;
using DemoUWP.Helpers;

namespace DemoUWP.ViewModels
{
    public class MasterDetailViewModel : Conductor<MasterDetailDetailViewModel>.Collection.OneActive
    {
        protected override async void OnInitialize()
        {
            base.OnInitialize();

            await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            Items.Clear();

            var data = await SampleDataService.GetMasterDetailDataAsync();

            Items.AddRange(data.Select(d => new MasterDetailDetailViewModel(d)));
        }
    }
}
