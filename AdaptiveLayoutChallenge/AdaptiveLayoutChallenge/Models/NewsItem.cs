using System;

namespace AdaptiveLayoutChallenge.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public NewsType Category { get; set; }
        public string Headline { get; set; }
        public string Subhead { get; set; }
        public string DateLine { get; set; }
        public string Image { get; set; }
    }

    public enum NewsType
    {
        Food = 1,
        Financial
    }
}
