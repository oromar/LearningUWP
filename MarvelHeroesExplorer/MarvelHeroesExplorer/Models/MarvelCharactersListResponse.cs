namespace MarvelHeroesExplorer.Models
{

    public class MarvelCharactersListResponse
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Result[] results { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Thumbnail thumbnail { get; set; }
    }

    public class Thumbnail
    {
        public string path { get; set; }
        public string extension { get; set; }
    }


}
