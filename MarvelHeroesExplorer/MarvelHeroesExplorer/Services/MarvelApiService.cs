using MarvelHeroesExplorer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace MarvelHeroesExplorer.Services
{
    public class MarvelApiService : IMarvelApiService
    {
        private const string PUBLIC_KEY = "";
        private const string PRIVATE_KEY = "";
        private const string BASE_URL = "http://gateway.marvel.com:80/v1/public/";

        private readonly HttpClient httpClient = new HttpClient();

        public MarvelApiService()
        {
        }

        private string AuthorizationData
        {
            get
            {
                var timestamp = DateTime.Now.Ticks;
                return $"ts={timestamp}&apikey={PUBLIC_KEY}&hash={Hash(timestamp)}";
            }
        }

        private static string Hash(long timestamp)
        {
            var toBeHashed = $"{timestamp}{PRIVATE_KEY}{PUBLIC_KEY}";
            return MD5(toBeHashed);
        }

        private static string MD5(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var buffer = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buffer);
            return CryptographicBuffer.EncodeToHexString(hashed);
        }

        public async Task<IList<MarvelCharacter>> GetCharacterListAsync(ApiFilters filter)
        {
            var timestamp = DateTime.Now.Ticks;

            var url = $"{BASE_URL}/characters?{AuthorizationData}&{filter}";

            var json = await SendHttpRequestAsync<MarvelCharactersListResponse>(url);

            var result = json.data.results.Select(hero => new MarvelCharacter
            {
                Id = hero.id,
                Name = hero.name,
                ImageURL = $"{hero.thumbnail.path}.{hero.thumbnail.extension}",
                Description = hero.description
            })
            .ToList();

            return result;
        }

        public async Task<List<MarvelComic>> GetCharacterComicsAsync(int id)
        {
            var timestamp = DateTime.Now.Ticks;
            var url = $"{BASE_URL}/characters/{id}/comics?{AuthorizationData}";

            var json = await SendHttpRequestAsync<MarvelComicListResponse>(url);

            var regex = new System.Text.RegularExpressions.Regex("</?\\w+>");

            var result = json.data.results.Where(comic => !string.IsNullOrWhiteSpace(comic.thumbnail.path)).Select(comic => new MarvelComic
            {
                Id = comic.id,
                Title = regex.Replace(comic?.title ?? string.Empty, string.Empty)?.Trim(),
                Description = regex.Replace(comic?.description ?? string.Empty, string.Empty)?.Trim(),
                ImageURL = $"{comic.thumbnail.path}.{comic.thumbnail.extension}",
                PublishedAt = DateTime.Parse(comic.dates.FirstOrDefault(a => a.type == "onsaleDate")?.date, null, System.Globalization.DateTimeStyles.RoundtripKind)
            }).ToList();

            return result;
        }

        private async Task<T> SendHttpRequestAsync<T>(string url)
        {
            var response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<T>(responseString);

            return json;
        }
    }
}
