using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SaveMan.Views
{
    public partial class ResetBalancePage : ContentPage
    {
        private string _balanchLabel;
        public string BalanchLabel
        {
            get
            {
                return _balanchLabel;
            }
            set
            {
                _balanchLabel = value;
                OnPropertyChanged(nameof(BalanchLabel));
            }
        }

        public ResetBalancePage()
        {
            BalanchLabel = "0";
            InitializeComponent();
            this.BindingContext = this;
        }

        public void CloseButton_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void NumberButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if(BalanchLabel == "0")
            {
                BalanchLabel = button.Text;
            }
            else
            {
                BalanchLabel += button.Text;
            }
        }

        public void DeleteButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
