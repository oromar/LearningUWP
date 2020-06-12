namespace MarvelHeroesExplorer.Models
{

    public class MarvelCharactersListResponse
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Result[] Results { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }

    public class Thumbnail
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }


}
