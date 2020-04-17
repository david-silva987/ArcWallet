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
            Navigation.PushAsync(new AddTransaction());

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listViewTransactions.ItemsSource = await App.Database.GetAllTransaction();
            balanceLabel.Text = await App.Database.GetBalance() + " CHF";

            // listViewDepense.ItemsSource = await App.Database.GetBiggestDepenseAsync();
            // totalRevenus.Text = await App.Database.GetAllRevenus();
            //  totalExpenditures.Text = await App.Database.dsadsaAsync();

            /*float totRevenus = float.Parse(totalRevenus.Text);
            float totDepenses = float.Parse(totalExpenditures.Text);
            float solde = totRevenus - totDepenses;
            balanceLabel.Text = solde.ToString();*/

        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {

            string UpdateOrDelete = await DisplayActionSheet ("Que souhaitez-vous faire?", "Modifier", "Supprimer");
            var content = e.Item as Transaction;

            if (UpdateOrDelete == "Supprimer")
            {
                string Yes = await DisplayActionSheet("Voulez-vous vraiment supprimer ce mouvement?", "Oui", "Non");

                if (Yes == "Oui")
                {
                    await App.Database.RemoveTransaction(content.ID);
                    listViewTransactions.ItemsSource = await App.Database.GetAllTransaction();
                    balanceLabel.Text = await App.Database.GetBalance() + " CHF";
                }
            }
            else if (UpdateOrDelete == "Modifier")
            {
                await Navigation.PushAsync(new UpdateTransaction(content));

                listViewTransactions.ItemsSource = await App.Database.GetAllTransaction();

                balanceLabel.Text = await App.Database.GetBalance() + " CHF";

            }
        }

    }
}