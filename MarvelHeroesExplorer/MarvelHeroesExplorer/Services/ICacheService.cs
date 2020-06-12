using System.Threading.Tasks;

namespace MarvelHeroesExplorer.Services
{
    public interface ICacheService
    {
        Task<T> GetCacheAsync<T>(string key);
        void WriteCacheAsync<T>(string key, T data);
    }
}
