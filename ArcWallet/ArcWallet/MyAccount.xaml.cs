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
    public partial class MyAccount : ContentPage
    {
        public MyAccount()
        {
            InitializeComponent();
        }

        async void addMouvementButton(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AjouterMouvement());

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listViewRevenu.ItemsSource = await App.Database.GetBiggestRevenuAsync();
            listViewDepense.ItemsSource = await App.Database.GetBiggestDepenseAsync();
            totalRevenus.Text = await App.Database.GetAllRevenus();
            totalExpenditures.Text = await App.Database.GetAllExpenditures();

            float totRevenus = float.Parse(totalRevenus.Text);
            float totDepenses = float.Parse(totalExpenditures.Text);
            float solde = totRevenus - totDepenses;
            balanceLabel.Text = solde.ToString();
           
        }
    }
}