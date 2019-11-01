using System;

using SQLite;

namespace SaveMan.Database
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
