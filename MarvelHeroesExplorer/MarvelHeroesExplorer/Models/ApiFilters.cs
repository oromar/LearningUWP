namespace MarvelHeroesExplorer.Models
{
    public class ApiFilters
    {
        public int Limit { get; set; } = 50;
        public int Offset { get; set; } = 0;

        public override string ToString()
        {
            return $"&limit={Limit}&offset={Offset}";
        }
    }
}
