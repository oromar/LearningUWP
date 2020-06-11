using System;

namespace MarvelHeroesExplorer.Models
{
    public class MarvelComic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime publishedAt;
        public DateTime PublishedAt
        {
            get { return publishedAt; }
            set
            {
                publishedAt = value;
                FormattedPublishedAt = value.ToString("MM/yyyy");
            }
        }
        public string ImageURL { get; set; }
        public string FormattedPublishedAt { get; set; }
    }
}
