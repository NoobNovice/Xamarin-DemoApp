using System;
using System.IO;
using SaveMan.Database;
using SaveMan.iOS.Database;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_iOS))]
namespace SaveMan.iOS.Database
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
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
