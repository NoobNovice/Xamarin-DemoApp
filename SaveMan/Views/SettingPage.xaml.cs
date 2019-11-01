using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SaveMan.Views
{
    public partial class SettingPage : ContentPage
    {
        public List<string> SettingLabel { get; set; }

        private Dictionary<string, int> _settingDict = new Dictionary<string, int>()
        {
            {"Reset Balance", 1},
            {"Clear History", 2}
        };

        public SettingPage()
        {
            SettingLabel = new List<string>()
            {
                "Reset Balance",
                "Clear History"
            };

            InitializeComponent();
            BindingContext = this;
        }

        public void LabelButton_Tapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            int selectIndex = _settingDict[label.Text];
            switch (selectIndex)
            {
                // Reset Balance
                case 1:
                    Navigation.PushModalAsync(new ResetBalancePage());
                    break;

                // Reset Database
                case 2:
                    App.TransactionDatabase.ClearTransaction();
                    App.TransactionLogDatabase.ClearLog();
                    App.HistoryMountDatabase.EmptyHistory();
                    break;
            }
            System.Diagnostics.Debug.WriteLine("End event");
        }

        public void BackButton_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
