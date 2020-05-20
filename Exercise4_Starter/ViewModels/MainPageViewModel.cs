using Exercise4.Commands;
using Exercise4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exercise4.ViewModels
{
    public class MainPageViewModel : NotificationBase
    {
        public ObservableCollection<Entry> Entries { get; set; }
        public Command AddCommand { get; set; }
        public Command RemoveCommand { get; set; }
        
        public MainPageViewModel()
        {
            Entries = new ObservableCollection<Entry>();
            AddCommand = new Command(Add, CanAdd);
            RemoveCommand = new Command(Remove, CanRemove);
        }
        
        private Entry entrySelected = null;
        public Entry EntrySelected
        {
            get
            {
                return entrySelected;
            }
            set
            {
                entrySelected = value;
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        private string inputValue = null;
        public string InputValue
        {
            get
            {
                return inputValue;
            }
            set
            {
                inputValue = value;
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public void Add()
        {
            Entries.Add(new Entry { Value = double.Parse(InputValue, CultureInfo.GetCultureInfo("pt-BR")), DateTime = DateTime.Now });
            inputValue = null;
            NotifyPropertyChanged(nameof(InputValue));
            NotifyPropertyChanged(nameof(Total));
            AddCommand.RaiseCanExecuteChanged();
        }

        public void Remove()
        {
            Entries.Remove(EntrySelected);
            EntrySelected = null;
            NotifyPropertyChanged(nameof(EntrySelected));
            NotifyPropertyChanged(nameof(Total));
            RemoveCommand.RaiseCanExecuteChanged();
        }

        public double Total
        {
            get
            {
                return Entries.Sum(a => a.Value);
            }
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(inputValue) && double.TryParse(inputValue, out double _);
        }

        private bool CanRemove()
        {
            return entrySelected != null;
        }
    }
}
