using ArcWallet.Interfaces;
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
            listViewTransactions.ItemsSource = await App.Database.GetAllTransactions();
            balanceLabel.Text = await App.Database.GetBalance() + " CHF";

            if((listViewTransactions.ItemsSource as List<Transaction>).Count ==0)
            {
                listViewTransactions.IsVisible = false;
                labelDerniersMouvements.IsVisible = false;
                labelNoTransactions.IsVisible = true;
            }

            
            if(await App.Database.GetBudget() != 0.0)
            {
                budgetLabel.Text = await App.Database.GetBudget() + " CHF";
                //budgetLabel.Text = await App.Database.GetSpentLastWeek() + "";
            }
            else
            {
                budgetLabel.Text = "Pas de budget";
            }

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
                    listViewTransactions.ItemsSource = await App.Database.GetAllTransactions();
                    if ((listViewTransactions.ItemsSource as List<Transaction>).Count == 0)
                    {
                        listViewTransactions.IsVisible = false;
                        labelDerniersMouvements.IsVisible = false;
                        labelNoTransactions.IsVisible = true;
                    }
                    balanceLabel.Text = await App.Database.GetBalance() + " CHF";
                    DependencyService.Get<IMessage>().LongAlert("Transaction supprimée avec succès");

                }
            }
            else if (UpdateOrDelete == "Modifier")
            {
                await Navigation.PushAsync(new UpdateTransaction(content));

                listViewTransactions.ItemsSource = await App.Database.GetAllTransactions();

                balanceLabel.Text = await App.Database.GetBalance() + " CHF";

            }
        }

    }
}