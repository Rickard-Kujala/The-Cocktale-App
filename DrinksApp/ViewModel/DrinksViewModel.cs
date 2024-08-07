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
        #region Public variables

        public ObservableCollection<Drink> Drinks { get; } = new();
        public Drink DrinkOfTheDay { get; set; }
        public bool IsRefreshing { get; set; }
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
                    FilterDrinks(); 
                }
            }
        }
        public bool IsNonAlcoholicFilterEnabled
        {
            get => _isNonAlcoholicFilterEnabled;
            set
            {
                if (_isNonAlcoholicFilterEnabled != value)
                {
                    _isNonAlcoholicFilterEnabled = value;
                    OnPropertyChanged();
                    FilterDrinks(); 
                }
            }
        }
        #endregion

        #region Private variables

        private bool _isAlcoholicFilterEnabled;
        private string _searchText;
        private ObservableCollection<Drink> _allDrinks; // To keep the original list
        private bool _isNonAlcoholicFilterEnabled;

        #endregion

        #region Private fields

        private readonly IDrinkService _drinkService;

        #endregion

        #region Public methods

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
        #endregion

        #region Private methods


        private void FilterDrinks()
        {
            //var filtered = _allDrinks.Where(d =>
            //    (string.IsNullOrWhiteSpace(SearchText) || d.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) &&
            //    (!IsAlcoholicFilterEnabled || d.Alcoholic == "Alcoholic")).ToList();

            var filtered = new List<Drink>();

            if (IsAlcoholicFilterEnabled)
            {
                filtered = FilterAlcoholicDrinks();
                PopulateDrinksList(filtered);

            }
            else if (IsNonAlcoholicFilterEnabled)
            {
                filtered = FilterNonAlcoholicDrinks();
                PopulateDrinksList(filtered);
            }
            else
            {
                Drinks.Clear();
                foreach (var drink in _allDrinks)
                {
                    Drinks.Add(drink);
                }
            }
        }

        private void PopulateDrinksList(List<Drink> drinks )
        {
            Drinks.Clear();
            foreach (var drink in drinks)
            {
                Drinks.Add(drink);
            }
        }

        private List<Drink> FilterNonAlcoholicDrinks()
        {
            List<Drink> filtered = _allDrinks.Where(d =>
                d.Alcoholic != Constants.Alcoholic).ToList();
            IsAlcoholicFilterEnabled = false;
            return filtered;
        }

        private List<Drink> FilterAlcoholicDrinks()
        {
            List<Drink> filtered = _allDrinks.Where(d =>
                                d.Alcoholic == Constants.Alcoholic).ToList();
            IsNonAlcoholicFilterEnabled = false;
            return filtered;
        }

        #endregion

    }
}
