using CommunityToolkit.Mvvm.ComponentModel;
using DrinksApp.Model;
using MonkeyFinder.ViewModel;
using Newtonsoft.Json;
using SQLite;

using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DrinksApp.Data;

namespace DrinksApp.ViewModel
{
    [QueryProperty(nameof(Drink), "Drink")]

    public partial  class DrinkDetailsViewModel : BaseViewModel
    {
        public DrinkDetailsViewModel(DrinksDatabase database)
        {
            _database = database;
        }
        public bool IsRefreshing { get; set; }
        [ObservableProperty]
        Drink drink;
        private readonly DrinksDatabase _database;

        [RelayCommand]
        async Task SaveAsync()
        {
            try
            {
                await _database.Save(Drink); 
                await Shell.Current.DisplayAlert("Success", "Drink Added successfully to library!", "OK");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("oops...something happened!", e.Message, "OK");
                //Some sort of logging
            }
        }
    }
}
