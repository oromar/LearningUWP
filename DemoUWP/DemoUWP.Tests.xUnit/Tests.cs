using System;

using Caliburn.Micro;

using DemoUWP.Services;
using DemoUWP.ViewModels;

using Moq;

using Xunit;

namespace DemoUWP.Tests.XUnit
{
    // TODO WTS: Add appropriate tests
    public class Tests
    {
        [Fact]
        public void TestMethod1()
        {
        }

        // TODO WTS: Add tests for functionality you add to ContentGridViewModel.
        [Fact]
        public void TestContentGridViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var mockNavService = new Mock<INavigationService>();
            var mockAnimationService = new Mock<IConnectedAnimationService>();
            var vm = new ContentGridViewModel(mockNavService.Object, mockAnimationService.Object);
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to DataGridViewModel.
        [Fact]
        public void TestDataGridViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new DataGridViewModel();
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to MainViewModel.
        [Fact]
        public void TestMainViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new MainViewModel();
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to MasterDetailViewModel.
        [Fact]
        public void TestMasterDetailViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new MasterDetailViewModel();
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to SettingsViewModel.
        [Fact]
        public void TestSettingsViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new SettingsViewModel();
            Assert.NotNull(vm);
        }
    }
}
