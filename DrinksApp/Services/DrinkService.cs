using DrinksApp.Clients;
using DrinksApp.Mappers;
using DrinksApp.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinksAppClient _client;
        private readonly IDrinkMapper _mapper;

        public DrinkService(IDrinksAppClient client, IDrinkMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        public async Task<List<Drink>> GetRandomDrinkAsync()
        {
            var resp = await _client.GetRandomDrinkRequestAsync();
            return MapToDrinks(resp.Content.Drinks);
        }

        public async Task<List<Drink>> GetByNameAsync(string name)
        {
            var resp = await _client.GetByNameAsync(name);
            return MapToDrinks(resp.Content.Drinks);

        }

        private List<Drink> MapToDrinks(List<DrinkResponse> resp)
        {
            var listOfDrinks = new List<Drink>();
            foreach (var drink in resp)
            {
                listOfDrinks.Add(_mapper.MapResponseToDrink(drink));
            }

            return listOfDrinks;
        }
    }
}
