using AlbumCoverGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AlbumCoverGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private readonly ObservableCollection<Song> Songs = new ObservableCollection<Song>();
        private List<StorageFile> AllSongs = new List<StorageFile>();
        private const int MAX_RETRIES = 3;
        private int UserRetries = 0;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadingProgressRing.IsActive = true;

            AllSongs = await SetupMusicListAsync();

            await PrepareNewGameAsync(AllSongs);

            LoadingProgressRing.IsActive = false;
        }

        private async Task PrepareNewGameAsync(List<StorageFile> allSongs)
        {
            Songs.Clear();
            LoadingProgressRing.IsActive = true;

            InitialGameState();

            //2. Choose random songs from library
            var songsToPlay = await PickRandomSongsAsync(allSongs);

            //3. Pluck off meta data from selected songs
            await PopulateSongListAsync(songsToPlay);

            await ChooseCurrentSongAsync();

            CountDown.Begin();

            LoadingProgressRing.IsActive = false;
        }

        private void InitialGameState()
        {
            UserRetries = 0;
            InstructionTextBlock.Text = "Go";
            ResultTextBlock.Text = "";
            AlbumTextBlock.Text = "";
            ArtistTextBlock.Text = "";
            TitleTextBlock.Text = "";
            PlayAgainButton.Visibility = Visibility.Collapsed;
            SongMediaElement.Stop();
        }

        private async Task ChooseCurrentSongAsync()
        {
            int random = new Random().Next(0, Songs.Count() - 1);
            var song = Songs[random];
            song.Selected = true;

            SongMediaElement.SetSource(await song.SongFile.OpenAsync(FileAccessMode.Read), "audio/mpeg");
        }

        private static async Task<List<StorageFile>> SetupMusicListAsync()
        {
            //1. Get access o Music Library
            var musicFolder = KnownFolders.MusicLibrary;
            var allSongs = new List<StorageFile>();
            await RetrieveFilesInFoldersAsync(allSongs, musicFolder);
            return allSongs;
        }

        private async Task PopulateSongListAsync(List<StorageFile> songsToPlay)
        {
            Songs.Clear();
            var songId = 0;
            foreach (var song in songsToPlay)
            {
                var properties = await song.Properties.GetMusicPropertiesAsync();
                var currentThumbnail = await song.GetThumbnailAsync(ThumbnailMode.MusicView, 200, ThumbnailOptions.UseCurrentScale);
                var albumCover = new BitmapImage();
                albumCover.SetSource(currentThumbnail);
                Songs.Add(new Song
                {
                    Id = ++songId,
                    Album = properties.Album,
                    Artist = properties.Artist,
                    Selected = false,
                    SongFile = song,
                    Title = properties.Title,
                    Cover = albumCover
                });
            }
        }

        private static async Task<List<StorageFile>> PickRandomSongsAsync(List<StorageFile> allSongs)
        {
            var random = new Random();
            var count = random.Next(10, 35);
            var result = new List<StorageFile>();
            for (int i = 0; i < count; i++)
            {
                var currentSong = allSongs[random.Next(allSongs.Count - 1)];

                var songToPlayMusicProperties = await currentSong.Properties.GetMusicPropertiesAsync();

                var isDuplicated = false;
                foreach (var song in result)
                {
                    var songProperties = await song.Properties.GetMusicPropertiesAsync();
                    if (string.IsNullOrEmpty(songToPlayMusicProperties.Album)
                        || songToPlayMusicProperties.Album == songProperties.Album)
                    {
                        isDuplicated = true;
                    }
                }

                if (!isDuplicated)
                    result.Add(currentSong);
            }
            return result;
        }

        private static async Task RetrieveFilesInFoldersAsync(List<StorageFile> result, StorageFolder folder)
        {
            foreach (var item in await folder.GetFilesAsync())
            {
                if (item.ContentType == "audio/mpeg")
                {
                    result.Add(item);
                }
            }
            foreach (var subFolder in await folder.GetFoldersAsync())
            {
                await RetrieveFilesInFoldersAsync(result, subFolder);
            }
        }

        private async void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserRetries++;
            if (UserRetries < MAX_RETRIES)
            {
                var selectedSong = (Song)e.ClickedItem;
                if (selectedSong.Selected)
                {
                    ResultTextBlock.Text = "You Won !";
                    PlayAgainButton.Visibility = Visibility.Visible;
                    UserRetries = 0;
                    CountDown.Stop();
                }
                else
                {
                    const string TRY_AGAIN = "Try Again";
                    ResultTextBlock.Text = TRY_AGAIN;
                    await Task.Delay(2000);
                    if (ResultTextBlock.Text == TRY_AGAIN)
                        ResultTextBlock.Text = "";
                }
            }
            else
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            var song = Songs.First(a => a.Selected);
            ResultTextBlock.Text = "Game Over";
            TitleTextBlock.Text = $"Title: {song.Title}";
            AlbumTextBlock.Text = $"Album: {song.Album}";
            ArtistTextBlock.Text = $"Artist: {song.Artist}";
            PlayAgainButton.Visibility = Visibility.Visible;
            SongMediaElement.Stop();
            CountDown.Stop();
        }

        private async void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            await PrepareNewGameAsync(AllSongs);
        }

        private void CountDown_Completed(object sender, object e)
        {
            GameOver();
        }
    }
}
