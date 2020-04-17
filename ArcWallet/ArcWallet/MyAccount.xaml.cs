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
            bool answer = await DisplayAlert("Action", "Que souhaitez-vous faire?", "Modifier", "Supprimer"); //true ->modifier ,false ->supprimer
            var content = e.Item as Transaction;
            Console.WriteLine(content.ID);

            //Corriger car quand on click a cote ça supprime aussi 
            if (!answer)
            {
                await App.Database.RemoveTransaction(content.ID);
                listViewTransactions.ItemsSource = await App.Database.GetAllTransaction();
            }
            else
            {
                await Navigation.PushAsync(new UpdateTransaction(content));

            }
        }

    }
}