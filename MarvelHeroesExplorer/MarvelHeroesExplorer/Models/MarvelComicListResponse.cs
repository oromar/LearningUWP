namespace MarvelHeroesExplorer.Models
{
    public class MarvelComicListResponse
    {
        public ComicData data { get; set; }
    }

    public class ComicData
    {
        public ComicResult[] results { get; set; }
    }

    public class ComicResult
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Date[] dates { get; set; }
        public Thumbnail thumbnail { get; set; }
    }
   
    public class Date
    {
        public string type { get; set; }
        public string date { get; set; }
    }
}
