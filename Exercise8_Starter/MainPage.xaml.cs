﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Exercise8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<StorageFile> Files { get; set; } = new ObservableCollection<StorageFile>();

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;

            this.Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            var transferManager = DataTransferManager.GetForCurrentView();
            transferManager.DataRequested -= TransferManager_DataRequested;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var transferManager = DataTransferManager.GetForCurrentView();
            transferManager.DataRequested += TransferManager_DataRequested;
        }

        private void TransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            request.Data.Properties.Title = "Files to share";
            request.Data.Properties.Description = "To Share files";
            request.Data.SetStorageItems(Files.Select(a => a));
        }

        private void OnSahre(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void Rectangle_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;

            e.DragUIOverride.Caption = "Copy";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
            e.DragUIOverride.IsContentVisible = true;
        }

        private async void Rectangle_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                var count = items.Count;
                var localFolder = ApplicationData.Current.LocalFolder;
                
                if (items.Any())
                {
                    foreach (var item in items)
                    {
                        var newFile = await (item as StorageFile).CopyAsync(localFolder, item.Name, NameCollisionOption.GenerateUniqueName);
                        Files.Add(newFile);
                    }
                }

                ShowToast(count);
            }
        }

        private static void ShowToast(int count)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            var node = template.GetElementsByTagName("text").First();
            node.InnerText = $"{count} files added to the list";
            var notification = new ToastNotification(template);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}
