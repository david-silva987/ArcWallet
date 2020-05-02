using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcWallet
{

    /// <summary>
    /// Class to display some user stats
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageStatistiques : ContentPage
    {
        public PageStatistiques()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method called when page is active to user
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //stat biggest expenditure
            listViewBiggestDepense.ItemsSource = await App.Database.GetBiggestExpenditure();
            List<Transaction> list = listViewBiggestDepense.ItemsSource as List<Transaction>;
            //check if information is available, if not => custom message
            if ((listViewBiggestDepense.ItemsSource as List<Transaction>).Count == 0)
            {
                listViewBiggestDepense.IsVisible = false;
                labelNoBiggestDepense.IsVisible = true;
            }

            //stat biggest receiving
            listViewBiggestRevenu.ItemsSource = await App.Database.GetBiggestRevenue();
            List<Transaction> list2 = listViewBiggestRevenu.ItemsSource as List<Transaction>;
            //check if information is available, if not => custom message
            if ((listViewBiggestRevenu.ItemsSource as List<Transaction>).Count == 0)
            {
                listViewBiggestRevenu.IsVisible = false;
               
                labelNoBiggestRevenu.IsVisible = true;
            }

            //stat spentbyCategory
            listViewSpentByCategory.ItemsSource = await App.Database.GetSpentByCategory();
            List<Transaction> list3 = listViewSpentByCategory.ItemsSource as List<Transaction>;
            //check if information is available, if not => custom message
            if ((listViewSpentByCategory.ItemsSource as List<Transaction>).Count == 0)
            {
                listViewSpentByCategory.IsVisible = false;
                labelNoTransactionsSpentByCategory.IsVisible = true;
            }
            Console.ReadLine();















        }


    }
}