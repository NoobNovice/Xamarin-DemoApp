using System;
using System.Collections.Generic;
using Xamarin.Forms;

using SaveMan.Database;
using SaveMan.Models;

namespace SaveMan.Database
{
    public class TransactionDatabaseController
    {
        private static object _locker = new object();

        SQLite.SQLiteConnection connection;

        public TransactionDatabaseController()
        {
            connection = DependencyService.Get<ISQLite>().GetConnection();
            connection.CreateTable<TransactionModel>();
        }

        public List<TransactionModel> GetTransaction()
        {
            lock (_locker)
            {
                if(connection.Table<TransactionModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return connection.Table<TransactionModel>().ToList();
                }
            }
        }

        public TransactionModel GetTransactionWithDate(int Id)
        {
            lock (_locker)
            {
                try
                {
                    return connection.Table<TransactionModel>().Where(x => x.ID == Id).FirstOrDefault();
                }
                catch(Exception e)
                {
                    throw new Exception("TransactionDatabaseController: GetTransactionWithDate at line 43 fail => " + e.Message);
                }
            }
        }

        public bool UpdateTransaction(TransactionModel data)
        {
            lock (_locker)
            {
               if (!String.IsNullOrWhiteSpace(data.Date))
               {
                    var hasData = connection.Table<TransactionModel>().Where(i => i.Date == data.Date).FirstOrDefault();
                    // insert data
                    if(hasData == null)
                    {
                        try
                        {
                            connection.Insert(data);
                            return true;
                        }
                        catch(Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("TransactionDatabaseController: UpdateTransaction at line 50 fail => " + e.Message);
                            return false;
                        }
                    }
                    // update date
                    else
                    {
                        try
                        {
                            connection.Update(data);
                            return true;
                        }
                        catch(Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("TransactionDatabaseController: UpdateTrancation at line 64 fail => " + e.Message);
                            return false;
                        }
                    }
               }
               else
               {
                    System.Diagnostics.Debug.WriteLine("TransactionDatabaseController: UpdateTransaction Fail => invalid data.Date");
                    return false;
                 
               }
            }
        }

        public bool ClearTransaction()
        {
            lock (_locker)
            {
                try
                {
                    connection.DeleteAll<TransactionModel>();
                    connection.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("TransactionDatabaseController: ClearTransaction at line 101 => " + e.Message);
                    return false;
                }
            } 
        }
    }
}
