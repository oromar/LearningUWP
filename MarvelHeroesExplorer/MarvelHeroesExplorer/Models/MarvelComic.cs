using System;

namespace MarvelHeroesExplorer.Models
{
    public class MarvelComic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedAt { get; set; }
        public string ImageURL { get; set; }
    }
}
