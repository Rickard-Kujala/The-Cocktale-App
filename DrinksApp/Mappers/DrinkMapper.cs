using DrinksApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.Mappers
{
    public class DrinkMapper : IDrinkMapper

    {
        public Drink MapResponseToDrink(DrinkResponse drinkFromApi)
        {
            return new Drink
            {
                Id = drinkFromApi.IdDrink,
                Name = drinkFromApi.StrDrink,
                AlternateName = drinkFromApi.StrDrinkAlternate,
                Tags = drinkFromApi.StrTags,
                VideoUrl = drinkFromApi.StrVideo,
                Category = drinkFromApi.StrCategory,
                IBA = drinkFromApi.StrIBA,
                Alcoholic = drinkFromApi.StrAlcoholic,
                GlassType = drinkFromApi.StrGlass,
                Instructions = drinkFromApi.StrInstructions,
                //InstructionsByLanguage = new Dictionary<string, string>
                //    {
                //        { "es", drinkFromApi.StrInstructionsES },
                //        { "de", drinkFromApi.StrInstructionsDE },
                //        { "fr", drinkFromApi.StrInstructionsFR },
                //        { "it", drinkFromApi.StrInstructionsIT },
                //        { "zh-Hans", drinkFromApi.StrInstructionsZH_HANS },
                //        { "zh-Hant", drinkFromApi.StrInstructionsZH_HANT }
                //    },
                ThumbnailUrl = drinkFromApi.StrDrinkThumb,
                Ingredients = MapIngredients(drinkFromApi),
                ImageSource = drinkFromApi.StrImageSource,
                ImageAttribution = drinkFromApi.StrImageAttribution,
                CreativeCommonsConfirmed = drinkFromApi.StrCreativeCommonsConfirmed,
                DateModified = drinkFromApi.DateModified != null ? DateTime.Parse(drinkFromApi.DateModified) : default

            };
        }
        private static List<Ingredient> MapIngredients(DrinkResponse drinkResponse)
        {
            var ingredients = new List<Ingredient>();

            for (int i = 1; i <= 15; i++)
            {
                var ingredientName = (string)drinkResponse.GetType().GetProperty($"StrIngredient{i}").GetValue(drinkResponse);
                var measure = (string)drinkResponse.GetType().GetProperty($"StrMeasure{i}").GetValue(drinkResponse);

                if (!string.IsNullOrEmpty(ingredientName))
                {
                    ingredients.Add(new Ingredient
                    {
                        Name = ingredientName,
                        Measure = measure
                    });
                }
                else
                {
                    break;
                }
            }

            return ingredients;
        }
        public List<Drink> MapToDbDrinkItem(List<DrinkDbItem> drinksFromDb, List<Ingredient> ingredients)
        {
            return drinksFromDb.Select(dbDrink =>
            {
                var drink = new Drink() { Id = dbDrink.Id, ThumbnailUrl = dbDrink.ThumbnailUrl, Instructions = dbDrink.Instructions };
                drink.Ingredients.AddRange(ingredients.Where(x => x.Id == drink.Id));
                return drink;
            }).ToList();
        }
    }
   
}
