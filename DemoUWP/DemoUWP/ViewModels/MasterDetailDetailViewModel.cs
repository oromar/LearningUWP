using System;

using Caliburn.Micro;

using DemoUWP.Core.Models;

namespace DemoUWP.ViewModels
{
    public class MasterDetailDetailViewModel : Screen
    {
        public MasterDetailDetailViewModel(SampleOrder item)
        {
            Item = item;
        }

        public SampleOrder Item { get; }
    }
}
