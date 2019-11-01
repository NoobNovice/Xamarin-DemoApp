using System;
using System.IO;
using SaveMan.Database;
using SaveMan.Droid.Database;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]

namespace SaveMan.Droid.Database
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {

        }

        public SQLite.SQLiteConnection GetConnection()
        {
            var sqlFileName = "database.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqlFileName);

            return new SQLite.SQLiteConnection(path);
        }
    }
}