using CommunityToolkit.Mvvm.Input;
using DrinksApp.Model;
using DrinksApp.Services;
using DrinksApp.View;
using MonkeyFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.ViewModel
{
    public partial class DrinksViewModel : BaseViewModel
    {
        private readonly IDrinkService _drinkService;
        public ObservableCollection<Drink> Drinks { get; } = new();
        private ObservableCollection<Drink> _allDrinks; // To keep the original list
        public Drink DrinkOfTheDay { get; set; }
        public bool IsRefreshing { get; set; }
        private bool _isAlcoholicFilterEnabled;
        private string _searchText;

        public DrinksViewModel(IDrinkService drinkService)
        {
            _drinkService = drinkService;
            DrinkOfTheDay = new Drink();
            _allDrinks = new ObservableCollection<Drink>();
        }

        [RelayCommand]
        async Task GoToDrinkDetails(Drink drink)
        {
            if (drink == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
            {
                {"Drink", drink }
            });
        }

        [RelayCommand]
        async Task GoToLibrary()
        {
            await Shell.Current.GoToAsync(nameof(LibraryPage), true);
        }

        [RelayCommand]
        async Task SearchAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsRefreshing = true;
                var drinks = await _drinkService.GetByNameAsync(SearchText);
                _allDrinks.Clear(); // Update the original list

                foreach (var drink in drinks)
                {
                    _allDrinks.Add(drink);
                }

                FilterDrinks(); // Apply filter after search
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        private void FilterDrinks()
        {
            //var filtered = _allDrinks.Where(d =>
            //    (string.IsNullOrWhiteSpace(SearchText) || d.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) &&
            //    (!IsAlcoholicFilterEnabled || d.Alcoholic == "Alcoholic")).ToList();

            var filtered = _allDrinks.Where(d =>
                d.Alcoholic == "Alcoholic").ToList();

            Drinks.Clear();
            foreach (var drink in filtered)
            {
                Drinks.Add(drink);
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterDrinks(); // Filter as the text changes
                }
            }
        }

        public bool IsAlcoholicFilterEnabled
        {
            get => _isAlcoholicFilterEnabled;
            set
            {
                if (_isAlcoholicFilterEnabled != value)
                {
                    _isAlcoholicFilterEnabled = value;
                    OnPropertyChanged();
                    FilterDrinks(); // Filter when checkbox state changes
                }
            }
        }

        [RelayCommand]
        async Task GetDrinksAsync()
        {
            if (IsBusy)
                return;

            try
            {
                var drinks = await _drinkService.GetRandomDrinkAsync();
                _allDrinks.Clear(); // Update the original list

                foreach (var drink in drinks)
                {
                    _allDrinks.Add(drink);
                }

                FilterDrinks(); // Apply filter after getting drinks
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
