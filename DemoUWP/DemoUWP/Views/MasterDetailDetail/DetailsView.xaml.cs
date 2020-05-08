using System;

using DemoUWP.ViewModels;

namespace DemoUWP.Views.MasterDetailDetail
{
    public sealed partial class DetailsView
    {
        public DetailsView()
        {
            InitializeComponent();
        }

        public MasterDetailDetailViewModel ViewModel => DataContext as MasterDetailDetailViewModel;
    }
}
