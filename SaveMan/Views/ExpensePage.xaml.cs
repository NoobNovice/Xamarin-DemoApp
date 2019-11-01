using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SaveMan.Models;
using SaveMan.Dictionary;

namespace SaveMan.Views
{
    public partial class ExpensePage : ContentPage
    {
        private Dictionary<string, int> _tagDic;

        public ExpensePage()
        {
            _tagDic = TagDictionaly.GetDic();

            InitializeComponent();
            BindingContext = this;

            MessagingCenter.Subscribe<AddExpenseCardPage, List<TransactionModel>>(this, "update-transaction", (sender, value) =>
            {
                System.Diagnostics.Debug.WriteLine("Message update-transaction is sended!!!");
                UpdateCard(value);
            });

            UpdateCard(App.TransactionDatabase.GetTransaction());
        }

        public void AddButton_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddExpenseCardPage());
        }

        public void ClearData_Tapped(object sender, EventArgs e)
        {
            App.TransactionDatabase.ClearTransaction();
            App.TransactionLogDatabase.ClearLog();
            UpdateCard(App.TransactionDatabase.GetTransaction());
        }

        public void OverViewButton_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OverviewPage());
        }

        private void UpdateCard(List<TransactionModel> transaction)
        {
            bodyPage.Children.Clear();
            if (transaction != null)
            {
                transaction.Sort((t1, t2) => t1.ID.CompareTo(t2.ID));
                for (int i = transaction.Count - 1; i >= 0; i--)
                {
                    var log = App.TransactionLogDatabase.GetLog(transaction[i].Date);
                    bodyPage.Children.Add(ExpenseCard(transaction[i].Date, transaction[i].TotalCost.ToString(), log));
                }
            }
        }

        private Frame ExpenseCard(String date, String totalCost, List<TransactionLogModel> detailData)
        {
            Frame card = new Frame();
            card.HasShadow = false;
            card.BorderColor = Color.FromHex("#f0f0f0");
            card.BackgroundColor = Color.FromHex("#fefefe");
            card.Margin = new Thickness(8);

            #region body Frame
            StackLayout bodyCard = new StackLayout();
            bodyCard.VerticalOptions = LayoutOptions.Center;

            // header card
            StackLayout headerCard = new StackLayout();
            headerCard.Orientation = StackOrientation.Horizontal;
            headerCard.Children.Add(new Label
                                    {
                                        Text = date,
                                        FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label)),
                                        HorizontalOptions = LayoutOptions.StartAndExpand,
                                        TextColor = Color.FromHex("#757575")
                                    });
            headerCard.Children.Add(new Label
                                    {
                                        Text = totalCost,
                                        FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label)),
                                        HorizontalOptions = LayoutOptions.End,
                                        TextColor = Color.FromHex("#757575")
                                    });
            bodyCard.Children.Add(headerCard);

            // detail card
            bodyCard.Children.Add(new BoxView
                                  {
                                        HeightRequest = 1,
                                        HorizontalOptions = LayoutOptions.Fill,
                                        Color = Color.FromHex("f0f0f0")
                                  });
            foreach(TransactionLogModel data in detailData)
            {
                StackLayout rowDetail = new StackLayout();
                rowDetail.Orientation = StackOrientation.Horizontal;

                var order = _tagDic.FirstOrDefault(x => x.Value == data.TagID).Key;
                rowDetail.Children.Add(new Label()
                                       {
                                            Text = String.IsNullOrEmpty(data.ShortNote)? order : data.ShortNote,
                                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                            HorizontalOptions = LayoutOptions.StartAndExpand,
                                            TextColor = Color.FromHex("757575"),
                                            Margin = new Thickness(8,1,1,0)
                                       });
                rowDetail.Children.Add(new Label()
                {
                    Text = data.Cost.ToString(),
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    HorizontalOptions = LayoutOptions.End,
                    TextColor = Color.FromHex("757575"),
                    Margin = new Thickness(0, 1, 1, 0)
                });

                bodyCard.Children.Add(rowDetail);
            }
            #endregion

            card.Content = bodyCard;
            return card;
        }
    }
}
