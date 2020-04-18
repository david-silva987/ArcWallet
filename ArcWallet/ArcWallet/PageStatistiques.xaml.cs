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
    public partial class PageStatistiques : ContentPage
    {
        public PageStatistiques()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //  listView.ItemsSource = await App.Database.GetRevenueAsync();
            listViewBiggestDepense.ItemsSource = await App.Database.GetBiggestExpenditure();

            listViewBiggestRevenu.ItemsSource = await App.Database.GetBiggestRevenu();
            listViewSpentByCategory.ItemsSource = await App.Database.GetSpentByCategory();

            mostUsedCategory.Text = await App.Database.GetMostUsedCategoryExpenditure();

            




        }


    }
}