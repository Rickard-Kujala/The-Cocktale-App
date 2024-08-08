using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp.Model
{
    public class Drink
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AlternateName { get; set; }
        public string Tags { get; set; }
        public string VideoUrl { get; set; }
        public string Category { get; set; }
        public string IBA { get; set; }
        public string Alcoholic { get; set; }
        public string GlassType { get; set; }
        public string Instructions { get; set; }
        //public Dictionary<string, string> InstructionsByLanguage { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public string ImageSource { get; set; }
        public string ImageAttribution { get; set; }
        public string CreativeCommonsConfirmed { get; set; }
        public DateTime DateModified { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string PhotoPath { get; set; }

    }
    public class DrinkDbItem
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string AlternateName { get; set; }
        public string Tags { get; set; }
        public string VideoUrl { get; set; }
        public string Category { get; set; }
        public string IBA { get; set; }
        public string Alcoholic { get; set; }
        public string GlassType { get; set; }
        public string Instructions { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ImageSource { get; set; }
        public string ImageAttribution { get; set; }
        public string CreativeCommonsConfirmed { get; set; }
        public DateTime DateModified { get; set; }
        public string Notes { get; set; } = string.Empty ;
        public string PhotoPath { get; set; } 
    }

}
