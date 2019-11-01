using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

using SaveMan.Views;
using SaveMan.Database;
using SaveMan.Models;
using System.IO;
using SaveMan.Setting;

namespace SaveMan
{
    public partial class App : Application
    {
        #region data controller
        private static TransactionDatabaseController _transactionDatabase;
        public static TransactionDatabaseController TransactionDatabase
        {
            get
            {
                if(_transactionDatabase == null)
                {
                    _transactionDatabase = new TransactionDatabaseController();
                    return _transactionDatabase;
                }
                else
                {
                    return _transactionDatabase;
                }
            }
        }

        private static TransactionLogDatabaseController _transactionLogDatabase;
        public static TransactionLogDatabaseController TransactionLogDatabase
        {
            get
            {
                if(_transactionLogDatabase == null)
                {
                    _transactionLogDatabase = new TransactionLogDatabaseController();
                    return _transactionLogDatabase;
                }
                else
                {
                    return _transactionLogDatabase;
                }
            }
        }

        private static HistoryMountDatabaseController _historyMountDatabaseController;
        public static HistoryMountDatabaseController HistoryMountDatabase
        {
            get
            {
                if (_historyMountDatabaseController == null)
                {
                    _historyMountDatabaseController = new HistoryMountDatabaseController();
                }
                return _historyMountDatabaseController;
            }
        }
        #endregion

        private static AppSetting _saveManSetting;
        public static AppSetting SaveManSetting
        {
            get
            {
                if(_saveManSetting == null)
                {
                    _saveManSetting = new AppSetting();
                    return _saveManSetting;
                }
                else
                {
                    return _saveManSetting;
                }
            }
        }

        public App()
        {
            InitializeComponent();

            // clear transaction when new mount
            if(!DateTime.Now.ToString("MMyyyy").Equals(SaveManSetting.CurrentMount))
            {
                System.Diagnostics.Debug.WriteLine("App: update CurrentMount in AppSetting");
                SaveManSetting.CurrentMount = DateTime.Now.ToString("MMyyyy");

                System.Diagnostics.Debug.WriteLine("App: insert new history for this mount");
                HistoryMountDatabase.CreateHistoryOfMount(new HistoryMountModel());

                System.Diagnostics.Debug.WriteLine("App: prepair clear transaction");
                TransactionDatabase.ClearTransaction();
                TransactionLogDatabase.ClearLog();
                System.Diagnostics.Debug.WriteLine("App: clear transaction successx");
            }
            // first use
            if (SaveManSetting.FirstUse)
            {
                // in next version show walkthouge
                System.Diagnostics.Debug.WriteLine("App: insert new history for this mount");
                HistoryMountDatabase.CreateHistoryOfMount(new HistoryMountModel());

                System.Diagnostics.Debug.WriteLine("App: update FirstUse in AppSetting");
                SaveManSetting.FirstUse = false;
            }

            MainPage = new NavigationPage(new SettingPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
