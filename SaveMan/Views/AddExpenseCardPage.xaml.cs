using System;
using System.Collections.Generic;
using System.Globalization;

using Xamarin.Forms;

using SaveMan.Dictionary;
using SaveMan.Models;

namespace SaveMan.Views
{
    public partial class AddExpenseCardPage : ContentPage
    {
        private Dictionary<string, int> _tagDictionary;

        // value to save file
        private int _tagID;
        private string _dateStr;
        private float _cost;
        private string _shortNote = "";

        public AddExpenseCardPage()
        {
            InitializeComponent();
            _tagDictionary = TagDictionaly.GetDic();
            BindingContext = this;
        }

        public void AddButton_Tapped(object sender, EventArgs e)
        {
            _dateStr = DateFilled.Date.ToString("dd MMM yyyy");
            var id = Int32.Parse(DateFilled.Date.ToString("dd"));
            try
            {
                _cost = float.Parse(CostFilled.Text, CultureInfo.InvariantCulture.NumberFormat);
                if(_tagID != 10)
                {
                    _cost = -_cost;
                }
            }
            catch(ArgumentNullException)
            {
                _cost = 0f;
            }
            if (!String.IsNullOrEmpty(NoteFilled.Text))
            {
                _shortNote = NoteFilled.Text;
            }
            var logID = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: ID " + id);
            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: Date " + _dateStr);
            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: Total cost " + _cost);

            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: Log " + logID);
            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: Cost " + _cost);
            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: Short note " + _shortNote);
            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: Date(ForeignKey) " + _dateStr);
            System.Diagnostics.Debug.WriteLine("AddExpenseCardPage: TagID " + _tagID);

            #region database handle
            // transaction and transactionLog database
            var transaction = App.TransactionDatabase.GetTransactionWithDate(id);
            // transaction already exist
            if (transaction != null)
            {
                var log = new TransactionLogModel();
                log.LogID = logID;
                log.Cost = _cost;
                log.ShortNote = _shortNote;
                log.ReferenceDate = _dateStr;
                log.TagID = _tagID;
                if (!App.TransactionLogDatabase.UpdateLog(log))
                {
                    DisplayAlert("Application Error", "Process add log fail", "OK");
                }

                transaction.TotalCost += _cost;
                if (!App.TransactionDatabase.UpdateTransaction(transaction))
                {
                    DisplayAlert("Application Error", "Process update transaction fail", "OK");
                }
            }
            // transaction not have create new transaction and insert log
            else
            {
                var newTransaction = new TransactionModel();
                newTransaction.ID = id;
                newTransaction.Date = _dateStr;
                newTransaction.TotalCost = _cost;
                if (!App.TransactionDatabase.UpdateTransaction(newTransaction))
                {
                    DisplayAlert("Application Error", "Process add new transaction fail", "OK");
                }

                var log = new TransactionLogModel();
                log.LogID = logID;
                log.Cost = _cost;
                log.ShortNote = _shortNote;
                log.ReferenceDate = _dateStr;
                log.TagID = _tagID;
                if (!App.TransactionLogDatabase.UpdateLog(log))
                {
                    DisplayAlert("Application Error", "Process add log fail", "OK");
                }
            }

            // historyMount database
            var mountHistory = App.HistoryMountDatabase.GetHistory(App.SaveManSetting.CurrentMount);
            if(_tagID != 10)
            {
                mountHistory.Payment += _cost;

                switch (_tagID)
                {
                    case 1:
                        mountHistory.costTag1 += -(_cost);
                        break;
                    case 2:
                        mountHistory.costTag2 += -(_cost);
                        break;
                    case 3:
                        mountHistory.costTag3 += -(_cost);
                        break;
                    case 4:
                        mountHistory.costTag4 += -(_cost);
                        break;
                    case 5:
                        mountHistory.costTag5 += -(_cost);
                        break;
                    case 6:
                        mountHistory.costTag6 += -(_cost);
                        break;
                    case 7:
                        mountHistory.costTag7 += -(_cost);
                        break;
                    case 8:
                        mountHistory.costTag8 += -(_cost);
                        break;
                    case 9:
                        mountHistory.costTag9 += -(_cost);
                        break;
                }
            }
            else
            {
                mountHistory.Income += _cost;
            }
            App.HistoryMountDatabase.UpdateHistory(mountHistory);
            #endregion

            var value = App.TransactionDatabase.GetTransaction();
            MessagingCenter.Send<AddExpenseCardPage, List<TransactionModel>>(this, "update-transaction", value);
            MessagingCenter.Send<AddExpenseCardPage, string>(this, "update-report", "");
            Navigation.PopAsync();
        }

        public void TagButton_Tapped(object sender, EventArgs e)
        {
            AbsoluteLayout absolutelayout = sender as AbsoluteLayout;
            foreach (var child in absolutelayout.Children)
            {
                if (child.GetType().Equals(typeof(Label)))
                {
                    var label = child as Label;

                    TagFilled.Text = label.Text;
                    _tagID = _tagDictionary[label.Text];
                    System.Diagnostics.Debug.WriteLine("AddExpenseCardPage:tag name " + label.Text);
                    System.Diagnostics.Debug.WriteLine("AddExpenseCardPage:tag ID " + _tagID);
                }
            }
        }

        public void BackButton_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
