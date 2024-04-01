using DrinksApp.Model;

namespace DrinksApp.Mappers
{
    public interface IDrinkResponseMapper
    {
        Drink MapResponseToDrink(DrinkResponse drink);
    }
}