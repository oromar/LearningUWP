using System.Threading.Tasks;

namespace MarvelHeroesExplorer.Services
{
    public interface ICacheService
    {
        Task<T> GetCacheAsync<T>(string key);
        void WriteCache<T>(string key, T data);
    }
}
