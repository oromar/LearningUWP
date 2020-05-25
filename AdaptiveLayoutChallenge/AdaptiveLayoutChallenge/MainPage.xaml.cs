using AdaptiveLayoutChallenge.Models;
using AdaptiveLayoutChallenge.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdaptiveLayoutChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<NewsItem> Items;
        public MainPage()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<NewsItem>();
            NewsService.GetNewsByCategory(NewsType.Financial).ToList().ForEach(a => Items.Add(a));
        }

        private void HamburguerButtonClick(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private void ListViewItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedItem = e.ClickedItem as StackPanel;
            Items.Clear();
            if (selectedItem.Name == nameof(FinancialMenuItem))
            {
                NewsService.GetNewsByCategory(NewsType.Financial).ForEach(a => Items.Add(a));
                TitleTextBlock.Text = "Financial";
            }
            else
            {
                NewsService.GetNewsByCategory(NewsType.Food).ForEach(a => Items.Add(a));
                TitleTextBlock.Text = "Food";
            }
            MenuSplitView.IsPaneOpen = false;
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Items.Clear();
            var seletedCategory = TitleTextBlock.Text == "Food" ? NewsType.Food : NewsType.Financial;
            var items = NewsService.GetNewsByCategory(seletedCategory).Where(e => e.Headline.ToLower().Contains(SearchBox.Text.ToLower()));
            items.ToList().ForEach(a => Items.Add(a));
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var seletedCategory = TitleTextBlock.Text == "Food" ? NewsType.Food : NewsType.Financial;
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                Items.Clear();
                NewsService.GetNewsByCategory(seletedCategory).ForEach(a => Items.Add(a));
            }
        }
    }
}
