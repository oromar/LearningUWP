using Prism.Mvvm;

namespace MarvelHeroesExplorer.Models
{
    public class MarvelCharacter : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _imageURL;
        public string ImageURL
        {
            get { return _imageURL; }
            set { SetProperty(ref _imageURL, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }


    }
}
