using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace MarvelHeroesExplorer.Services
{
    public class CacheService : ICacheService
    {
        private readonly StorageFolder applicationFolder;

        public CacheService()
        {
            applicationFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var files = await applicationFolder.GetFilesAsync();

            if (files.Any(a => a.DisplayName == key))
            {
                var file = files.First(a => a.DisplayName == key);
                if (DateTime.Now.Ticks - file.DateCreated.Ticks < TimeSpan.FromDays(5).Ticks) // 5 days cache
                    return JsonConvert.DeserializeObject<T>(File.ReadAllText(file.Path));
            }
            return default(T);
        }

        public async void WriteCacheAsync<T>(string key, T data)
        {
            await File.WriteAllTextAsync(Path.Combine(applicationFolder.Path, $"{key}.json"), JsonConvert.SerializeObject(data));
        }
    }
}
