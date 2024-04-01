using DrinksApp.Mappers;
using DrinksApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DrinksApp.Data
{
    public class DrinksDatabase
    {
        SQLiteAsyncConnection Database;
        private readonly IDrinkMapper _drinkMapper;

        public DrinksDatabase(IDrinkMapper drinkMapper)
        {
            _drinkMapper = drinkMapper;
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<DrinkDbItem>();
            var result2 = await Database.CreateTableAsync<Ingredient>();
        }
        public async Task Save(Drink drink)
        {
            await Init();
            var ingredients = new List<Ingredient>();   
            foreach (var ingredient in drink.Ingredients)
            {
                ingredient.Id = drink.Id;
                ingredients.Add(ingredient);
            }

            await Database.InsertAllAsync(ingredients);
            await Database.InsertAsync(new DrinkDbItem {Id= drink.Id, ThumbnailUrl= drink.ThumbnailUrl, Instructions= drink.Instructions });    
        }
        public async Task<List<Drink>> GetAllDrinks()
        {
            await Init();
            var ingredients = await Database.Table<Ingredient>().ToListAsync();
            var drinksFromDb = await Database.Table<DrinkDbItem>().ToListAsync();
            return _drinkMapper.MapToDbDrinkItem(drinksFromDb, ingredients);
        }
    }
}
