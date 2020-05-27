using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWPSoundBoard.Models;
using UWPSoundBoard.Services;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
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

        private Sound _soundToAdd;

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
            if (string.IsNullOrWhiteSpace(sender.Text))
            {
                Items.Clear();
                SoundService.GetAllSounds().ForEach(a => Items.Add(a));
                SearchAutoSuggestBox.ItemsSource = null;
            }
            else
            {
                var allSounds = SoundService.GetAllSounds();
                SearchAutoSuggestBox.ItemsSource = allSounds.Where(a => a.Name.ToLower().Contains(sender.Text.ToLower())).Select(b => b.Name);
            }
            CategoryTextBlock.Text = "All Sounds";
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Items.Clear();
            SoundService.GetSounds(sender.Text).ForEach(a => Items.Add(a));
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
            var audioURI = soundItem.Category == SoundCategory.Custom ? soundItem.AudioFile : $"ms-appx://{soundItem.AudioFile}";
            MyMediaElement.Source = new Uri(audioURI);
            MyMediaElement.Play();
        }

        private async void SoundGridView_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    var contentType = storageFile.ContentType;

                    var localFolder = ApplicationData.Current.LocalFolder;

                    if (contentType == "image/png" || contentType == "image/jpeg")
                    {
                        var newFile = await storageFile.CopyAsync(localFolder, storageFile.Name, NameCollisionOption.GenerateUniqueName);
                        if (_soundToAdd == null)
                        {
                            _soundToAdd = new Sound
                            {
                                Name = storageFile.Name,
                                ImageFile = newFile.Path,
                                Category = SoundCategory.Custom
                            };
                        }
                        else
                        {
                            _soundToAdd.ImageFile = newFile.Path;
                        }
                    }
                    else if (contentType == "audio/wav" || contentType == "audio/mpeg")
                    {
                        var newFile = await storageFile.CopyAsync(localFolder, storageFile.Name, NameCollisionOption.GenerateUniqueName);
                        if (_soundToAdd == null)
                        {
                            _soundToAdd = new Sound
                            {
                                Name = string.Empty,
                                AudioFile = newFile.Path,
                                ImageFile = string.Empty,
                                Category = SoundCategory.Custom
                            };
                        }
                        else
                        {
                            _soundToAdd.AudioFile = newFile.Path;
                        }

                        MyMediaElement.SetSource(await storageFile.OpenAsync(FileAccessMode.Read), contentType);
                        MyMediaElement.Play();
                    }
                    AddSoundIfComplete();
                }
            }
        }

        private void AddSoundIfComplete()
        {
            if (_soundToAdd != null &&
                !string.IsNullOrWhiteSpace(_soundToAdd.AudioFile) &&
                !string.IsNullOrWhiteSpace(_soundToAdd.ImageFile))
            {
                Items.Clear();
                SoundService.AddSound(_soundToAdd);
                SoundService.GetAllSounds().ForEach(a => Items.Add(a));
                _soundToAdd = null;
            }
        }

        private void SoundGridView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;

            e.DragUIOverride.Caption = "Drop to add Sound";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }
    }
}
