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
            List<Transaction> list = listViewBiggestDepense.ItemsSource as List<Transaction>;
            Console.WriteLine(list.Count);
            if ((listViewBiggestDepense.ItemsSource as List<Transaction>).Count == 0)
            {
                listViewBiggestDepense.IsVisible = false;
                labelNoBiggestDepense.IsVisible = true;
            }


            listViewBiggestRevenu.ItemsSource = await App.Database.GetBiggestRevenue();
            List<Transaction> list2 = listViewBiggestRevenu.ItemsSource as List<Transaction>;
            Console.WriteLine(list2.Count);
            if ((listViewBiggestRevenu.ItemsSource as List<Transaction>).Count == 0)
            {
                listViewBiggestRevenu.IsVisible = false;
               
                labelNoBiggestRevenu.IsVisible = true;
            }
            listViewSpentByCategory.ItemsSource = await App.Database.GetSpentByCategory();
            List<Transaction> list3 = listViewSpentByCategory.ItemsSource as List<Transaction>;
            Console.WriteLine(list3.Count);
            if ((listViewSpentByCategory.ItemsSource as List<Transaction>).Count == 0)
            {
                listViewSpentByCategory.IsVisible = false;
                labelNoTransactionsSpentByCategory.IsVisible = true;
            }
            Console.ReadLine();















        }


    }
}