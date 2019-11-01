using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Microcharts;
using SkiaSharp;

using SaveMan.Models;

namespace SaveMan.Views
{
    public partial class OverviewPage : ContentPage
    {
        public DonutChart OverviewChart { get; set; }

        private  HistoryMountModel _history;

        public OverviewPage()
        {
            UpdateChart();

            _history = App.HistoryMountDatabase.GetHistory(App.SaveManSetting.CurrentMount);

            InitializeComponent();
            BindingContext = this;

            incomeLabel.Text = _history.Income.ToString();
            outcomeLabel.Text = _history.Payment.ToString();

            float percent = GetPercentage(_history.Payment, _history.costTag1);
            percentFood.Text =  percent.Equals(0)? "" : percent.ToString() + "%";
            costFood.Text = (Math.Round((decimal)_history.costTag1,2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag2);
            percentTransport.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costTransport.Text = (Math.Round((decimal)_history.costTag2, 2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag3);
            percentShopping.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costShopping.Text = (Math.Round((decimal)_history.costTag3, 2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag4);
            percentEntertainment.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costEntertainment.Text = (Math.Round((decimal)_history.costTag4, 2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag5);
            percentTravel.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costTravel.Text = (Math.Round((decimal)_history.costTag5, 2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag6);
            percentLearning.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costLearning.Text = (Math.Round((decimal)_history.costTag6, 2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag7);
            percentHome.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costHome.Text = (Math.Round((decimal)_history.costTag7, 2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag8);
            percentInsurance.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costInsurance.Text = (Math.Round((decimal)_history.costTag8, 2) + 0.00m).ToString();

            percent = GetPercentage(_history.Payment, _history.costTag9);
            percentMedical.Text = percent.Equals(0) ? "" : percent.ToString() + "%";
            costMedical.Text = (Math.Round((decimal)_history.costTag9, 2) + 0.00m).ToString();
        }

        public void BackButton_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void UpdateChart()
        {
            _history = App.HistoryMountDatabase.GetHistory(App.SaveManSetting.CurrentMount);

            if(_history.Payment.Equals(0))
            {
                OverviewChart = new DonutChart()
                {
                    Entries = new[]
                {
                    new Microcharts.Entry(100)
                    {
                        Color = SKColor.Parse("#EEEEEE")
                    }
                },

                    HoleRadius = 0.8f,
                };
            }
            else
            {
                OverviewChart = new DonutChart()
                {
                    Entries = new[]
                    {
                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag1))
                        {
                            Color = SKColor.Parse("#DF4E3B")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag2))
                        {
                            Color = SKColor.Parse("#473786")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag3))
                        {
                            Color = SKColor.Parse("#409193")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag4))
                        {
                            Color = SKColor.Parse("#E76939")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag5))
                        {
                            Color = SKColor.Parse("#230C3D")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag6))
                        {
                            Color = SKColor.Parse("#4CA55F")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag7))
                        {
                            Color = SKColor.Parse("#E2903D")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag8))
                        {
                            Color = SKColor.Parse("#973470")
                        },

                        new Microcharts.Entry(GetPercentage(_history.Payment,_history.costTag9))
                        {
                            Color = SKColor.Parse("#9FBB57")
                        },
                    },

                    HoleRadius = 0.8f,
                };
            } 
        }

        private float GetPercentage(float maxValue, float value)
        {
            try
            {
                decimal dec = (Decimal)((value * 100) / -maxValue);

                var result = Math.Round(dec, 2);

                return (float)result;
            }
            catch (OverflowException)
            {
                return 0f;
            }
        }
    }
}