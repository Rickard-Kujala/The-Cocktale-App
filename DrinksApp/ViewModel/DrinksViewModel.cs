using MonkeyFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder.ViewModel
{
    public partial class DrinksViewModel : BaseViewModel
    {
        private readonly IDrinkService _drinkService;

        public Drink RandomDrink{ get; set; }
        public DrinksViewModel(IDrinkService drinkService)
        {
            RandomDrink = new Drink();
            Title = "Happy drink day!";
            _drinkService = drinkService;
        }

        [RelayCommand]
        async Task GetRandomDrink()
        {
            var resp = await _drinkService.GetRandomDrinkAsync();
            
        }
    }
}
