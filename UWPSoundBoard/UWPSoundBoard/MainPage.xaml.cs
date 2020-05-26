using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWPSoundBoard.Models;
using UWPSoundBoard.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPSoundBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> Items { get; } = new ObservableCollection<Sound>();
        private List<MenuItem> MenuItems { get; } = new List<MenuItem>();

        public MainPage()
        {
            this.InitializeComponent();
            SoundService.GetAllSounds().ForEach(a => Items.Add(a));

            MenuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/animals.png",
                Category = SoundCategory.Animals
            });
            MenuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/cartoon.png",
                Category = SoundCategory.Cartoons
            });
            MenuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/taunt.png",
                Category = SoundCategory.Taunts
            });
            MenuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/warning.png",
                Category = SoundCategory.Warnings
            });
        }

        private void HamburguerButton_Click(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAutoSuggestBox.Text = string.Empty;
            MenuListView.SelectedItem = null;
            SearchAutoSuggestBox_TextChanged(SearchAutoSuggestBox, null);
        }

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var criteria = sender.Text;
            if (!string.IsNullOrWhiteSpace(criteria))
            {
                Items.Clear();
                SoundService.GetSounds(criteria).ForEach(a => Items.Add(a));
            }
            else
            {
                Items.Clear();
                SoundService.GetAllSounds().ForEach(a => Items.Add(a));
            }
            CategoryTextBlock.Text = "All Sounds";
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SearchAutoSuggestBox_TextChanged(sender, null);
        }

        private void MenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            Items.Clear();
            SoundService.GetSounds(menuItem.Category).ForEach(a => Items.Add(a));
            CategoryTextBlock.Text = menuItem.Category.ToString();
            MenuSplitView.IsPaneOpen = false;
        }

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var soundItem = e.ClickedItem as Sound;
            MyMediaElement.Source = new Uri($"ms-appx://{soundItem.AudioFile}");
            MyMediaElement.Play();
        }
    }
}
