using System;

using DemoUWP.ViewModels;

namespace DemoUWP.Views.MasterDetailDetail
{
    public sealed partial class MasterView
    {
        public MasterView()
        {
            InitializeComponent();
        }

        public MasterDetailDetailViewModel ViewModel => DataContext as MasterDetailDetailViewModel;
    }
}
