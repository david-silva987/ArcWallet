using ArcWallet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcWallet
{
    /// <summary>
    /// Class to update a transaction after clicking on it in listView
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateTransaction : ContentPage
    {
        Transaction transaction = new Transaction();

        public bool False { get; }

        public UpdateTransaction()
        {
            InitializeComponent();
            transactionPicker.SelectedIndex = 0;
            categoryPicker.SelectedIndex = 0;
        }

        /// <summary>
        /// Check in runtime which type of transaction is selected, so some informations are shown or hiden
        /// </summary>
        /// <param name="sender">The picker</param>
        /// <param name="e"></param>
        public void transactionType_SelectedIndexChanged(object sender, EventArgs e)
        {

            var picker = sender as Picker;
            if (picker.Items[picker.SelectedIndex].Equals("Dépense"))
            {
                categoryLabel.IsVisible = true;
                categoryPicker.IsVisible = true;
            }
            else
            {
                categoryLabel.IsVisible = false;
                categoryPicker.IsVisible = false;

            }
        }

        /// <summary>
        /// Method that updates a transaction
        /// </summary>
        /// <param name="oldTransaction">Old transaction, the one which was clicked in listView</param>
        public UpdateTransaction(Transaction oldTransaction)
        {
            this.transaction = oldTransaction;

            InitializeComponent();
            if (oldTransaction.Type)
            {
                transactionPicker.SelectedIndex = 1;
                
            }

            else
            {
                transactionPicker.SelectedIndex = 0;
                categoryPicker.SelectedItem = oldTransaction.Category;

            }

            nameEntry.Text = oldTransaction.Name;
            dateEntry.Date = DateTime.Parse(oldTransaction.Date);
            AmoutEntry.Text = oldTransaction.Amount.ToString();
        }

        /// <summary>
        /// Action onClick button to update a transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void updateTransactionButton(object sender, EventArgs e)
        {
            bool transactionType;
            string categorySelected;

            if (transactionPicker.SelectedItem.ToString().Equals("Dépense"))
            {
                transactionType = false;
                categorySelected = categoryPicker.SelectedItem.ToString();
            }
            else
            {
                transactionType = true;
                categorySelected = "Revenu";
            }

            if(CheckFormValid())
            {
                await App.Database.UpdateTransaction(new Transaction
                {
                    ID = transaction.ID, //since we update, use same ID as odl transaction
                    Type = transactionType,
                    Name = nameEntry.Text,
                    Category = categorySelected,
                    Date = dateEntry.Date.ToString(),
                    Amount = float.Parse(AmoutEntry.Text),

                });
                await Navigation.PushAsync(new TabbedMyAccount());
             
                DependencyService.Get<IMessage>().LongAlert("Transaction modifiée avec succès"); //success message
            }
            else
            {
                Console.WriteLine("Pas Ok");
                DependencyService.Get<IMessage>().ShortAlert("Entrée non valide"); //form is not valid
            }

        }

        /// <summary>
        /// Check if all entries in form are valid
        /// </summary>
        /// <returns></returns>
        private bool CheckFormValid()
        {
            if (transactionPicker.SelectedItem.ToString().Equals("Dépense")) //if transaction is an expenditure
            {
                return CheckName() && CheckCategory() && CheckAmount();
            }
            else
            {
                return CheckName() && CheckAmount(); //if it's a receiving
            }

        }

        /// <summary>
        /// Check if name is not null or empty
        /// </summary>
        /// <returns></returns>
        private bool CheckName()
        {
            return !string.IsNullOrEmpty(nameEntry.Text);
        }

        /// <summary>
        /// Checki if a category is chosen in picker
        /// </summary>
        /// <returns></returns>
        private bool CheckCategory()
        {
            return !string.IsNullOrEmpty(categoryPicker.SelectedItem.ToString());
        }

        /// <summary>
        /// Check if amount is given and it's a number
        /// </summary>
        /// <returns></returns>
        private bool CheckAmount()
        {
            return !string.IsNullOrEmpty(AmoutEntry.Text) && AmoutEntry.Text != "." && !AmoutEntry.Text.Contains("-");
        }
    }
}