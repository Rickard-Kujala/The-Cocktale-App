using DrinksApp.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.Clients
{
    public interface IDrinksAppClient
    {
        [Get("/api/json/v1/1/random.php")]
        Task<ApiResponse<FromApi>> GetRandomDrinkRequestAsync();

        [Get("/api/json/v1/1/search.php?s={name}")]
        Task<ApiResponse<FromApi>> GetByNameAsync(string name);
    }
}
