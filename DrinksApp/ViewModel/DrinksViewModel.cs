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
        public Drink DrinkOfTheDay { get; set; }
        public bool IsRefreshing { get; set; }

        public DrinksViewModel(IDrinkService drinkService)
        {
            _drinkService = drinkService;
            DrinkOfTheDay = new Drink();
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
        async Task ShearchAsync()
        {

            if (IsBusy)
                return;

            try
            {
                IsRefreshing  = true;
                var drinks = await _drinkService.GetByNameAsync(SearchText);
                Drinks.Clear();

                foreach (var drink in drinks)
                {
                    Drinks.Add(drink); 
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }

        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
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
                Drinks.Clear(); 
                foreach (var drink in drinks)
                {
                    Drinks.Add(drink);
                }
                
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }

        }
    }
}
