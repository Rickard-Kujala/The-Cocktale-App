using DrinksApp.Model;

namespace DrinksApp.Data
{
    public interface IDrinksRepository
    {
        Task Delete(Drink drink);
        Task DeleteAll();
        Task<List<Drink>> GetAllDrinks();
        Task Save(Drink drink);
    }
}