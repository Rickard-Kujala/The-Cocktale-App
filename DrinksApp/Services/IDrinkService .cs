using DrinksApp.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.Services
{
    public interface IDrinkService
    {
        Task<List<Drink>> GetRandomDrinkAsync();
        Task<List<Drink>> GetByNameAsync(string name);
    }
}
