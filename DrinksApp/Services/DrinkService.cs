//using MonkeyFinder.Clients;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MonkeyFinder.Services
//{

//    public class DrinkService : IDrinkService
//    {
//        private readonly IDrinksAppClient _client;

//        public DrinkService(IDrinksAppClient client)
//        {
//            _client = client;
//        }
//        public async Task<DrinkResponse> GetRandomDrinkAsync()
//        {
//            var resp = await _client.GetRandomDrinkRequest();
//            return resp.Content;
//        }
//    }
//}
