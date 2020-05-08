using System;
using System.Linq;
using System.Threading.Tasks;

using DemoUWP.Core.Services;

using Xunit;

namespace DemoUWP.Core.Tests.XUnit
{
    // TODO WTS: Add appropriate unit tests.
    public class Tests
    {
        [Fact]
        public void Test1()
        {
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Fact]
        public async void EnsureSampleDataServiceReturnsContentGridDataAsync()
        {
            var actual = await SampleDataService.GetContentGridDataAsync();

            Assert.NotEmpty(actual);
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Fact]
        public async void EnsureSampleDataServiceReturnsGridDataAsync()
        {
            var actual = await SampleDataService.GetGridDataAsync();

            Assert.NotEmpty(actual);
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Fact]
        public async Task EnsureSampleDataServiceReturnsMasterDetailDataAsync()
        {
            var actual = await SampleDataService.GetMasterDetailDataAsync();

            Assert.NotEmpty(actual);
        }
    }
}
