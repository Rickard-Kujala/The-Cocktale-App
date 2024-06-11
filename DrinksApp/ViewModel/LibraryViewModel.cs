using CommunityToolkit.Mvvm.Input;
using DrinksApp.Data;
using DrinksApp.Model;
using DrinksApp.View;
using MonkeyFinder.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.ViewModel
{
    public partial class LibraryViewModel : BaseViewModel
    {
        public ObservableCollection<Drink> SavedDrinks { get; } = new();

        private readonly DrinksDatabase _database;

        public bool IsRefreshing { get; set; }

        public LibraryViewModel(DrinksDatabase database)
        {
            _database = database;
            GetAllDrinks();
        }
        [RelayCommand]
        async Task DeleteAllAsync()
        {
            try
            {
                await _database.DeleteAll();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("oops...something happened!", e.Message, "OK");
            }
        }
        public async void GetAllDrinks()
        {
            try
            {
                var drinksFromDb = await _database.GetAllDrinks();
                foreach (var drink in drinksFromDb)
                {
                    SavedDrinks.Add(drink);
                }
            }
            catch (Exception e)
            {


            }
            
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
    }
}
