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
        public DrinkDetailsViewModel(IDrinksRepository repo)
        {
            _repo = repo;
        }
        public bool IsRefreshing { get; set; }
        [ObservableProperty]
        Drink drink;
        private readonly IDrinksRepository _repo;

        [RelayCommand]
        async Task SaveAsync()
        {
            try
            {
                await _repo.Save(Drink); 
                await Shell.Current.DisplayAlert("Success", "Drink Added successfully to library!", "OK");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("oops...something happened!", e.Message, "OK");
                //Some sort of logging
            }
        }
        [ObservableProperty]
        private bool showInstructions;

        [ObservableProperty]
        private bool showIngredients;

        [ObservableProperty]
        private string toggleInstructionsBtnText;

        [ObservableProperty]
        private string toggleIngredientsBtnText;

        //public DrinkDetailsViewModel()
        //{
        //    ShowInstructions = true;
        //    ShowIngredients = true;
        //}

        [RelayCommand]
        private void ToggleInstructions()
        {
            ShowInstructions = !ShowInstructions;
            ToggleInstructionsBtnText = ShowInstructions ? "˄" : "˅";
        }


        [RelayCommand]
        private void ToggleIngredients()
        {
            ShowIngredients = !ShowIngredients;
            ToggleIngredientsBtnText = ShowIngredients ? "˄" : "˅";
        }
    }
}
    