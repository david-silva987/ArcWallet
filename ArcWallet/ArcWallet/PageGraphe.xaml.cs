
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace ArcWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGraphe : ContentPage
    {


        public PageGraphe()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // listView.ItemsSource = await App.Database.GetExpenditureAsync();
            totalSpentBinding.Text = await App.Database.GetMoneySpent();
            totalReceivedBinding.Text = await App.Database.GetMoneyReceîved();

        }





    }
}