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
            listView.ItemsSource = await App.Database.GetExpenditureAsync();
        }


        async void addExpenditureButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(locationEntry.Text) && !string.IsNullOrWhiteSpace(AmoutEntry.Text))
            {
                await App.Database.SaveExpenditureAsync(new Expenditure
                {
                    Location = locationEntry.Text,
                    Category = categoryEntry.SelectedItem.ToString(),
                    Date = dateEntry.Date.ToString(),
                    Time = timeEntry.Time.ToString(),
                    Amount = float.Parse(AmoutEntry.Text),

                });

                locationEntry.Text = AmoutEntry.Text = string.Empty;
                listView.ItemsSource = await App.Database.GetExpenditureAsync();
            }
        }
    }
}

