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
    /// <summary>
    /// Class MyAccount, the first one to be displayed
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccount : ContentPage
    {
        public MyAccount()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Action OnClick to add a transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void addMouvementButton(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTransaction());

        }

        /// <summary>
        /// Method called when page is active to user
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listViewTransactions.ItemsSource = await App.Database.GetAllTransactions(); //interaction with db to get all conection
            balanceLabel.Text = await App.Database.GetBalance() + " CHF";

            bool typeBudget = await App.Database.GetTypeBudget();

            //design text while checking what kind of budget we have
            if(typeBudget)
            {
                budgetTitlelbl.Text = "Budget Hebdomadaire";
            }
            else
            {
                budgetTitlelbl.Text = "Budget Mensuel";
            }

            //if it's empty, custom messages are displayed
            if((listViewTransactions.ItemsSource as List<Transaction>).Count ==0)
            {
                listViewTransactions.IsVisible = false;
                labelDerniersMouvements.IsVisible = false;
                labelNoTransactions.IsVisible = true;
            }

            
            if(await App.Database.GetBudget() != 0.0)
            {
                budgetLabel.Text = await App.Database.GetBudget() + " CHF";
            }
            else
            {
                budgetLabel.Text = "Pas de budget";
            }

        }


        /// <summary>
        /// Called when User clicks on a transaction in the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Item clicked</param>
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