using MarvelHeroesExplorer.Models;
using MarvelHeroesExplorer.Services;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Windows.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MarvelHeroesExplorer.ViewModels
{
    public class HeroesPageViewModel : ViewModelBase
    {
        private readonly IMarvelApiService marvelService;
        private readonly ICacheService cacheService;

        public HeroesPageViewModel(IMarvelApiService marvelApiService, ICacheService cacheService)
        {
            this.marvelService = marvelApiService;
            this.cacheService = cacheService;
            Init();
        }

        private readonly IList<MarvelCharacter> allHeroes = new List<MarvelCharacter>();

        private ObservableCollection<MarvelCharacter> _heroes = new ObservableCollection<MarvelCharacter>();
        public ObservableCollection<MarvelCharacter> Heroes
        {
            get { return _heroes; }
            set { SetProperty(ref _heroes, value); }
        }

        private IList<MarvelComic> _comics = default(IList<MarvelComic>);
        public IList<MarvelComic> Comics
        {
            get { return _comics; }
            set { SetProperty(ref _comics, value); }
        }

        private MarvelCharacter _selectedHero;
        public MarvelCharacter SelectedHero
        {
            get { return _selectedHero; }
            set { SetProperty(ref _selectedHero, value); }
        }

        private bool _loadingHeroes;
        public bool LoadingHeroes
        {
            get { return _loadingHeroes; }
            set
            {
                SetProperty(ref _loadingHeroes, value);
                HeroesLoaded = !value;
            }
        }

        private bool _heroesLoaded;
        public bool HeroesLoaded
        {
            get { return _heroesLoaded; }
            set { SetProperty(ref _heroesLoaded, value); }
        }

        private bool _loadingComics;
        public bool LoadingComics
        {
            get { return _loadingComics; }
            set
            {
                SetProperty(ref _loadingComics, value);
                ComicsLoaded = !value;
            }
        }

        private bool _initFinished = false;
        public bool InitFinished
        {
            get { return _initFinished; }
            set { SetProperty(ref _initFinished, value); }
        }

        private bool _initLoading;
        public bool InitLoading
        {
            get { return _initLoading; }
            set 
            { 
                SetProperty(ref _initLoading, value);
                InitFinished = !value;
            }
        }


        private bool _comicsLoaded;
        public bool ComicsLoaded
        {
            get { return _comicsLoaded; }
            set { SetProperty(ref _comicsLoaded, value); }
        }

        public async void OnSelectHero(object sender, ItemClickEventArgs e)
        {
            LoadingComics = true;

            var clickedItem = (MarvelCharacter)e.ClickedItem;
            SelectedHero = Heroes.First(a => a.Id == clickedItem.Id);
            var data = await GetCharacterComicsDataAysnc(SelectedHero.Id);

            Comics = data;

            LoadingComics = false;
        }

        public void OnSearchHero(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            sender.IsSuggestionListOpen = false;
            if (string.IsNullOrWhiteSpace(sender.Text))
            {
                ResetHeroesList();
                return;
            }
            
            var list = allHeroes.Where(hero => hero.Name.ToLower().Contains(sender.Text.ToLower())).ToList();
            Heroes.Clear();
            list.ForEach(hero => Heroes.Add(hero));
        }

        public void OnSearchTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(sender.Text)) ResetHeroesList();
            else OnSearchHero(sender, null);
        }

        private void ResetHeroesList()
        {
            Heroes.Clear();
            allHeroes.ForEach(a => Heroes.Add(a));
        }

        private async Task Init()
        {
            LoadingHeroes = InitLoading = true;

            var filter = new ApiFilters();

            Heroes.Clear();

            var data = await GetCharactersDataAsync(filter);

            while (data.Count > 0)
            {
                data.ForEach(hero => Heroes.Add(hero));
                data.ForEach(hero => allHeroes.Add(hero));
                LoadingHeroes = false;
                filter.Offset += 50;
                data = await GetCharactersDataAsync(filter);
            }
            InitLoading = false;
        }

        private async Task<IList<MarvelComic>> GetCharacterComicsDataAysnc(int id)
        {
            IList<MarvelComic> data;

            var key = $"Hero_Comics_{id}";

            var cache = await cacheService.GetCacheAsync<List<MarvelComic>>(key);

            if (cache != null)
            {
                data = cache;
            }
            else
            {
                data = await marvelService.GetCharacterComicsAsync(id);
                cacheService.WriteCacheAsync<IList<MarvelComic>>(key, data);
            }

            return data;
        }

        private async Task<IList<MarvelCharacter>> GetCharactersDataAsync(ApiFilters filter)
        {
            IList<MarvelCharacter> data;

            var key = $"Heroes_{filter.Offset}_{filter.Limit + filter.Offset}";
            var cache = await cacheService.GetCacheAsync<List<MarvelCharacter>>(key);
            if (cache != null)
            {
                data = cache;
            }
            else
            {
                data = await marvelService.GetCharacterListAsync(filter);
                cacheService.WriteCacheAsync<IList<MarvelCharacter>>(key, data);
            }
            return data;
        }
    }
}
