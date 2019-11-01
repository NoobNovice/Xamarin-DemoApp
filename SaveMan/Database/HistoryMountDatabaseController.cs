using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using SaveMan.Models;

namespace SaveMan.Database
{
    public class HistoryMountDatabaseController
    {
        private static object _locker;
        private SQLiteConnection _connection;

        public HistoryMountDatabaseController()
        {
            _locker = new object();
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<HistoryMountModel>();
        }

        public void CreateHistoryOfMount(HistoryMountModel history)
        {
            lock (_locker)
            {
                try
                {
                    _connection.Insert(history);
                    _connection.Commit();
                }
                catch(SQLiteException e)
                {
                    throw new Exception("HistoryMountDatabaseController: CreateHistoryOfMount at line 26 fail => " + e.Message);
                }
            }
        }

        public List<HistoryMountModel> GetHistoryMounts()
        {
            lock (_locker)
            {
                try
                {
                    return _connection.Table<HistoryMountModel>().ToList();
                }
                catch(SQLiteException e)
                {
                    throw new Exception("HistoryMountDatabaseController: CreateHistoryOfMount at line 43 fail => " + e.Message);
                }
            }
        }

        public HistoryMountModel GetHistory(string historyID)
        {
            lock (_locker)
            {
                try
                {
                    return _connection.Table<HistoryMountModel>().Where(x => x.HistoryID == historyID).FirstOrDefault();
                }
                catch(SQLiteException e)
                {
                    throw new Exception("HistoryMountDatabaseController: GetHistoryMount at line 58 fail => " + e.Message);
                } 
            }
        }

        public void UpdateHistory(HistoryMountModel history)
        {
            lock (_locker)
            {
                try
                {
                    _connection.Update(history);
                }
                catch(SQLiteException e)
                {
                    throw new Exception("HistoryMountDatabaseController: UpdateHistory at line 73 fail => " + e.Message);
                }
            }
        }

        public void DeleteHistory(string historyID)
        {
            lock (_locker)
            {
                try
                {
                    _connection.Table<HistoryMountModel>().Delete(x => x.HistoryID == historyID);
                }
                catch(SQLiteException e)
                {
                    throw new Exception("HistoryMountDatabaseController: GetHistoryMount at line 88 fail => " + e.Message);
                }
            }
        }

        public void EmptyHistory()
        {
            lock (_locker)
            {
                _connection.DeleteAll<HistoryMountModel>();
                _connection.Commit();
            }
        }
    }
}
