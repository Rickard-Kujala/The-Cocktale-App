using DrinksApp.Model;

namespace DrinksApp.Mappers
{
    public interface IDrinkMapper
    {
        Drink MapResponseToDrink(DrinkResponse drink);
        List<Drink> MapToDbDrinkItem(List<DrinkDbItem> drinksFromDb, List<Ingredient> ingredients);
    }
}