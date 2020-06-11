using MarvelHeroesExplorer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelHeroesExplorer.Services
{
    public interface IMarvelApiService
    {
        Task<IList<MarvelCharacter>> GetCharacterListAsync(ApiFilters filter);
        Task<List<MarvelComic>> GetCharacterComicsAsync(int id);
    }
}
