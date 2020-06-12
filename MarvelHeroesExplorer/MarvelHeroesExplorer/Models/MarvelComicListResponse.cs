namespace MarvelHeroesExplorer.Models
{
    public class MarvelComicListResponse
    {
        public ComicData Data { get; set; }
    }

    public class ComicData
    {
        public ComicResult[] Results { get; set; }
    }

    public class ComicResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ComicDate[] Dates { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
   
    public class ComicDate
    {
        public string Type { get; set; }
        public string Date { get; set; }
    }
}
