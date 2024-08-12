using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksApp
{
    public static class Constants
    {
        public const string Alcoholic = "Alcoholic";
        //public const string UpArrowFilePath= "Resources/Images/up.svg";
        //public const string DownArrowFilePath= "Resources/Images/down.svg";
        public const string UpArrowFilePath = "up";
        public const string DownArrowFilePath = "down";
        public const string DatabaseFilename = "DrinksSQLite.db3";
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
