using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcWallet
{
    public partial class addExpenditure : ContentPage
    {
        public addExpenditure()
        {
            InitializeComponent();
           

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }


        async void addExpenditureButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(AmoutEntry.Text))
            {
                await App.Database.SaveExpenditureAsync(new Expenditure
                {
                    Name = nameEntry.Text,
                    Category = categoryEntry.SelectedItem.ToString(),
                    Date = dateEntry.Date.ToString(),
                    Amount = float.Parse(AmoutEntry.Text),

                });

                nameEntry.Text = AmoutEntry.Text = string.Empty;
                await Navigation.PushAsync(new TabbedMyAccount());
            }
        }
    }
}

