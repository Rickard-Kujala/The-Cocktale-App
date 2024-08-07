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
    public class DrinksRepository : IDrinksRepository
    {
        private SQLiteAsyncConnection _database;
        private readonly IDrinkMapper _drinkMapper;

        public DrinksRepository(IDrinkMapper drinkMapper)
        {
            _drinkMapper = drinkMapper;
        }

        async Task Init()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _database.CreateTableAsync<DrinkDbItem>();
            var result2 = await _database.CreateTableAsync<Ingredient>();
        }
        public async Task DeleteAll()
        {
            await Init();
            await _database.DeleteAllAsync<Ingredient>();
            await _database.DeleteAllAsync<DrinkDbItem>();
        }

        public async Task Delete(Drink drink)
        {
            await Init();
            var itemToDelete = await _database.Table<DrinkDbItem>().FirstOrDefaultAsync(x => x.Id == drink.Id);
            await _database.DeleteAsync<DrinkDbItem>(itemToDelete.Id);

            var ingredientsToDelete = await _database.Table<Ingredient>().Where(x => x.Id == drink.Id).ToListAsync();
            foreach (var item in ingredientsToDelete)
            {
                await _database.DeleteAsync<Ingredient>(item.Id);
            }
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

            await _database.InsertAllAsync(ingredients);
            await _database.InsertAsync(new DrinkDbItem
            {
                Id = drink.Id,
                ThumbnailUrl = drink.ThumbnailUrl,
                Instructions = drink.Instructions,
                AlternateName
                = drink.AlternateName,
                Name = drink.Name
            });
        }
        public async Task<List<Drink>> GetAllDrinks()
        {
            await Init();
            var ingredients = await _database.Table<Ingredient>().ToListAsync();
            var drinksFromDb = await _database.Table<DrinkDbItem>().ToListAsync();
            return _drinkMapper.MapToDbDrinkItem(drinksFromDb, ingredients);
        }
        public async void Update(Drink drink)
        {
            await Init();
            var drinkToUpdate = await  _database.Table<DrinkDbItem>().FirstOrDefaultAsync(d => d.Id == drink.Id);
            drinkToUpdate.Notes = drink.Notes;
            await _database.UpdateAsync(drinkToUpdate);
        }
        public async Task<Drink> GetWithId(string id)
        {
            await Init();
            var drinks = await GetAllDrinks();
            return drinks.FirstOrDefault(d => d.Id == id);
        }
    }
}
