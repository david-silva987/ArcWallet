using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjouterRevenu : ContentPage
    {
        public AjouterRevenu()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetRevenueAsync();
        }


        async void addRevenuButton(object sender, EventArgs e)
        {
            int ordre = 0;
            if (categoryEntry.SelectedItem.ToString().Equals("Permanent"))
            {
                ordre = 1;
            }


            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(AmoutEntry.Text))
            {
                await App.Database.SaveRevenuAsycn(new Revenue
                {
                    Name = nameEntry.Text,
                    Permanent = ordre,
                    Date = dateEntry.Date.ToString(),
                    Amount = float.Parse(AmoutEntry.Text),

                }) ;

                nameEntry.Text = AmoutEntry.Text = string.Empty;
                listView.ItemsSource = await App.Database.GetRevenueAsync();
            }
        }
    }
}