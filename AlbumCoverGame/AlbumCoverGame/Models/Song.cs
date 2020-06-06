using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace AlbumCoverGame.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public IStorageFile SongFile { get; set; }
        public bool Selected { get; set; }
        public BitmapImage Cover { get; set; }
    }
}
