﻿using Exercise4.Commands;
using Exercise4.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml.Input;

namespace Exercise4.ViewModels
{
    public class MainPageViewModel : BaseViewModel
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

        public double Total
        {
            get
            {
                return Entries.Sum(a => a.Value);
            }
        }

        public void OnKeyUp(object sender, KeyRoutedEventArgs evt)
        {
            if (evt.Key == Windows.System.VirtualKey.Enter)
            {
                AddCommand.Execute(null);
            }
        }

        private void Add()
        {
            Entries.Add(new Entry { Value = double.Parse(InputValue), DateTime = DateTime.Now });
            InputValue = null;
            NotifyPropertyChanged(nameof(InputValue));
            NotifyPropertyChanged(nameof(Total));
            AddCommand.RaiseCanExecuteChanged();
        }

        private void Remove()
        {
            Entries.Remove(EntrySelected);
            EntrySelected = null;
            NotifyPropertyChanged(nameof(EntrySelected));
            NotifyPropertyChanged(nameof(Total));
            RemoveCommand.RaiseCanExecuteChanged();
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
