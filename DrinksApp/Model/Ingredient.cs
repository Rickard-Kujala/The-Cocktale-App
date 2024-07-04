using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.Model
{
    public class Ingredient
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
    }
}
