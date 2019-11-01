using System;
using System.Collections.Generic;
using SQLite;

using Xamarin.Forms;
using SaveMan.Models;

namespace SaveMan.Database
{
    public class TransactionLogDatabaseController
    {
        private static object _locker = new object();

        SQLite.SQLiteConnection connection;

        public TransactionLogDatabaseController()
        {
            connection = DependencyService.Get<ISQLite>().GetConnection();
            connection.CreateTable<TransactionLogModel>();
        }

        public List<TransactionLogModel> GetLog(string date)
        {
            lock (_locker)
            {
                var log = connection.Table<TransactionLogModel>().Where(i => i.ReferenceDate == date).ToList();
                // insert log
                if (log == null)
                {
                    return null;
                }
                // update log
                else
                {
                    return log;
                }
            }
        }

        public bool UpdateLog(TransactionLogModel log)
        {
            lock (_locker)
            {
                var hasData = connection.Table<TransactionLogModel>().Where(i => i.LogID == log.LogID).FirstOrDefault();
                if (hasData == null)
                {
                    try
                    {
                        connection.Insert(log);
                        return true;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("TransactionLogDatabaseController: UpdateLog at line 47 fail => " + e.Message);
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        connection.Update(log);
                        return true;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("TransactionLogDatabaseController: UpdateLog at line 60 fail => " + e.Message);
                        return false;
                    }
                }
            }
        }

        public bool DeleteLog(TransactionLogModel log)
        {
            lock (_locker)
            {
                try
                {
                    connection.Delete(log);
                    connection.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("TransactionLogDatabaseController: DeleteLog at line 80 => " + e.Message);
                    return false;
                }
            }
        }

        public bool ClearLog()
        {
            lock (_locker)
            {
                connection.DeleteAll<TransactionLogModel>();
                connection.Commit();
                return true;
            }
        }
    }
}
