using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Microcharts;
using SkiaSharp;

using SaveMan.Models;

namespace SaveMan.Views
{
    public partial class AnalyzePage : ContentPage
    {
        public LineChart SavingChart { get; set; }
        public LineChart FoodChart { get; set; }
        public LineChart TransportChart { get; set; }
        public LineChart ShoppingChart { get; set; }
        public LineChart EntertainmentChart { get; set; }
        public LineChart TravelChart { get; set; }

        public string IncomeAvg { get; set; }
        public string OutcomeAvg { get; set; }
        public string FoodAvg { get; set; }
        public string TransportAvg { get; set; }
        public string ShoppingAvg { get; set; }
        public string EntertainmentAvg { get; set; }
        public string TravelAvg { get; set; }

        private string[] _hexColorEntries =
        {
            "#1A3B53",
            "#285A75",
            "#357796",
            "#4394B6",
            "#6EA594",
            "#A6B675",
            "#E6C85D",
            "#E3AB50",
            "#E28F43",
            "#E17338",
            "#CA5730",
            "#B13B29"
        };

        public AnalyzePage()
        {
            UpdateReport();

            InitializeComponent();
            BindingContext = this;

            MessagingCenter.Subscribe<AddExpenseCardPage, string>(this, "update-report", (sender, value) =>
            {
                System.Diagnostics.Debug.WriteLine("Message update-report is sended!!!");
                UpdateReport();
            });
        }

        private void UpdateReport()
        {
            System.Diagnostics.Debug.WriteLine("AnalyzePage: UpdateReport is called");
            var mountList = App.HistoryMountDatabase.GetHistoryMounts();
            var mountCount = mountList.Count;
            var maximunEntries = mountCount < 12 ? mountCount : 12;
            
            Microcharts.Entry[] savingEntries = new Microcharts.Entry[maximunEntries];
            Microcharts.Entry[] foodEntries = new Microcharts.Entry[maximunEntries];
            Microcharts.Entry[] transportEntries = new Microcharts.Entry[maximunEntries];
            Microcharts.Entry[] shoppingEntries = new Microcharts.Entry[maximunEntries];
            Microcharts.Entry[] entertainmentEntries = new Microcharts.Entry[maximunEntries];
            Microcharts.Entry[] travelEntries = new Microcharts.Entry[maximunEntries];

            #region generate entries
            int indexEntries = 0;
            for (int i = mountList.Count - maximunEntries; i < mountList.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("AnalyzePage: mountList[{0}].HistoryID = {1}", i, mountList[i].HistoryID);
                savingEntries[indexEntries] = new Microcharts.Entry(mountList[i].Income - mountList[i].Payment);
                if (indexEntries == 0 || indexEntries == maximunEntries - 1)
                {
                    savingEntries[indexEntries].Label = mountList[i].Mount + " " + mountList[i].Year;
                }
                savingEntries[indexEntries].ValueLabel = (mountList[i].Income - mountList[i].Payment).ToString();
                savingEntries[indexEntries].Color = SKColor.Parse(_hexColorEntries[indexEntries]);
                savingEntries[indexEntries].TextColor = SKColor.Parse(_hexColorEntries[indexEntries]);

                foodEntries[indexEntries] = new Microcharts.Entry(mountList[i].costTag1);
                if (indexEntries == 0 || indexEntries == maximunEntries - 1)
                {
                    foodEntries[indexEntries].Label = mountList[i].Mount + " " + mountList[i].Year;
                }
                foodEntries[indexEntries].ValueLabel = mountList[i].costTag1.ToString();
                foodEntries[indexEntries].Color = SKColor.Parse(_hexColorEntries[indexEntries]);
                foodEntries[indexEntries].TextColor = SKColor.Parse(_hexColorEntries[indexEntries]);

                transportEntries[indexEntries] = new Microcharts.Entry(mountList[i].costTag2);
                if (indexEntries == 0 || indexEntries == maximunEntries - 1)
                {
                    transportEntries[indexEntries].Label = mountList[i].Mount + " " + mountList[i].Year;
                }
                transportEntries[indexEntries].ValueLabel = mountList[i].costTag2.ToString();
                transportEntries[indexEntries].Color = SKColor.Parse(_hexColorEntries[indexEntries]);
                transportEntries[indexEntries].TextColor = SKColor.Parse(_hexColorEntries[indexEntries]);

                shoppingEntries[indexEntries] = new Microcharts.Entry(mountList[i].costTag3);
                if (indexEntries == 0 || indexEntries == maximunEntries - 1)
                {
                    shoppingEntries[indexEntries].Label = mountList[i].Mount + " " + mountList[i].Year;
                }
                shoppingEntries[indexEntries].ValueLabel = mountList[i].costTag3.ToString();
                shoppingEntries[indexEntries].Color = SKColor.Parse(_hexColorEntries[indexEntries]);
                shoppingEntries[indexEntries].TextColor = SKColor.Parse(_hexColorEntries[indexEntries]);

                entertainmentEntries[indexEntries] = new Microcharts.Entry(mountList[i].costTag4);
                if (indexEntries == 0 || indexEntries == maximunEntries - 1)
                {
                    entertainmentEntries[indexEntries].Label = mountList[i].Mount + " " + mountList[i].Year;
                }
                entertainmentEntries[indexEntries].ValueLabel = mountList[i].costTag4.ToString();
                entertainmentEntries[indexEntries].Color = SKColor.Parse(_hexColorEntries[indexEntries]);
                entertainmentEntries[indexEntries].TextColor = SKColor.Parse(_hexColorEntries[indexEntries]);

                travelEntries[indexEntries] = new Microcharts.Entry(mountList[i].costTag5);
                if (indexEntries == 0 || indexEntries == maximunEntries - 1)
                {
                    travelEntries[indexEntries].Label = mountList[i].Mount + " " + mountList[i].Year;
                }
                travelEntries[indexEntries].ValueLabel = mountList[i].costTag5.ToString();
                travelEntries[indexEntries].Color = SKColor.Parse(_hexColorEntries[indexEntries]);
                travelEntries[indexEntries].TextColor = SKColor.Parse(_hexColorEntries[indexEntries]);

                indexEntries++;
            }
            #endregion

            #region create LineChart
            SavingChart = new LineChart()
            {
                Entries = savingEntries,
                LabelTextSize = 30
            };
            FoodChart = new LineChart()
            {
                Entries = foodEntries,
                LabelTextSize = 30
            };
            TransportChart = new LineChart()
            {
                Entries = transportEntries,
                LabelTextSize = 30
            };
            ShoppingChart = new LineChart()
            {
                Entries = shoppingEntries,
                LabelTextSize = 30
            };
            EntertainmentChart = new LineChart()
            {
                Entries = entertainmentEntries,
                LabelTextSize = 30
            };
            TravelChart = new LineChart()
            {
                Entries = travelEntries,
                LabelTextSize = 30
            };
            #endregion

            #region assing avg label
            float income = 0;
            float outcome = 0;
            float foodCost = 0;
            float transportCost = 0;
            float shoppingCost = 0;
            float entertainmentCost = 0;
            float travelCost = 0;

            foreach (HistoryMountModel mount in mountList)
            {
                income += mount.Income;
                outcome += mount.Payment;
                foodCost += mount.costTag1;
                transportCost += mount.costTag2;
                shoppingCost += mount.costTag3;
                entertainmentCost += mount.costTag4;
                travelCost += mount.costTag5;
            }

            IncomeAvg = (income / mountCount).ToString();
            OutcomeAvg = (outcome / mountCount).ToString();
            FoodAvg = (foodCost / mountCount).ToString();
            TransportAvg = (transportCost / mountCount).ToString();
            ShoppingAvg = (shoppingCost / mountCount).ToString();
            EntertainmentAvg = (entertainmentCost / mountCount).ToString();
            TravelAvg = (travelCost / mountCount).ToString();
            #endregion
        }
    }
}
