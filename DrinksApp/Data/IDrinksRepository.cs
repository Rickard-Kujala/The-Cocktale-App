using DrinksApp.Model;

namespace DrinksApp.Data
{
    public interface IDrinksRepository
    {
        Task Delete(Drink drink);
        Task DeleteAll();
        Task DeletePhoto(string id);
        Task<List<Drink>> GetAllDrinks();
        Task<Drink> GetWithId(string id);
        Task Save(Drink drink);
        void Update(Drink drink);
    }
}