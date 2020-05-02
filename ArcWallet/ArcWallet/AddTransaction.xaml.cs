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
    /// Class AddTransaction, to let user add any kind of transactions
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransaction : ContentPage
    {
        public AddTransaction()
        {
            InitializeComponent();

            //Select by default the first item of picker
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
            
            //"Dépense" Selected
            if (picker.Items[picker.SelectedIndex].Equals("Dépense"))
            {
                categoryLabel.IsVisible = true;
                categoryPicker.IsVisible = true;
            }
            //"Revenu" Selected
            else
            {
                categoryLabel.IsVisible = false;
                categoryPicker.IsVisible = false;
            }
        }

        /// <summary>
        /// OnClick action when button is clicked.
        /// Lets user add a transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void addTransactionButton(object sender, EventArgs e)
        {
            //If form is valid
            if (CheckFormValid())
            {
                float spentLastWeek = float.Parse(await App.Database.GetSpentLastXDays()); //return 0 if budget is not defined
                float budget = await App.Database.GetBudget();
                /*Check if:
                 * transactionPicker' selected item is "Dépense"
                 * Date of transaction is not before last 7 days
                 * Budget is not equal to 0 (because 0 -> budget is not defined)
                 * Last 7 Days spent's amount + amount in form  is bigger than budget)
                 */

                bool typeBudget = await App.Database.GetTypeBudget();
                float days;
                if(typeBudget)
                {
                    days = -7;
                }
                else
                {
                    days = -30;
                }


                if (dateEntry.Date.Date > DateTime.Now.Date.AddDays(days) && transactionPicker.SelectedItem.ToString().Equals("Dépense") && budget != 0 && spentLastWeek + float.Parse(AmoutEntry.Text) > budget)
                {
                    string BudgetCheck = await DisplayActionSheet("Budget dépassé. Souhaitez-vous tout de même poursuivre la transaction?", "Oui", "Non");

                    if (BudgetCheck == "Oui")
                    {
                        AddTransactionToDB();
                    }
      
                }
                else
                {
                    AddTransactionToDB();
                }
            }
            //Form is not valid
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Entrée non valide");
            }
        }

        /// <summary>
        /// Add Transaction to database
        /// </summary>
        private async void AddTransactionToDB()
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

            await App.Database.SaveTransactionAsycn(new Transaction
            {
                Type = transactionType,
                Name = nameEntry.Text,
                Category = categorySelected,
                Date = dateEntry.Date.ToString(),
                Amount = float.Parse(AmoutEntry.Text),

            });

            DependencyService.Get<IMessage>().LongAlert("Transaction ajoutée avec succès");
            await Navigation.PushAsync(new TabbedMyAccount());
        }

        /// <summary>
        /// Check if form is valid
        /// Summary of all the validations done
        /// </summary>
        /// <returns></returns>
        private bool CheckFormValid()
        {
            if (transactionPicker.SelectedItem.ToString().Equals("Dépense"))
            {
                return CheckName() && CheckCategory() && CheckAmount();
            }
            else
            {
                return CheckName() && CheckAmount();
            }
        }

        /// <summary>
        /// Check if Name entered isn't null or empty
        /// </summary>
        /// <returns></returns>
        private bool CheckName()
        {
            return !string.IsNullOrEmpty(nameEntry.Text);
        }

        /// <summary>
        /// Check if a category is chosen
        /// </summary>
        /// <returns></returns>
        private bool CheckCategory()
        {
            return !string.IsNullOrEmpty(categoryPicker.SelectedItem.ToString());
        }

        /// <summary>
        /// Check if Amount is not null or empty and it's a number
        /// </summary>
        /// <returns></returns>
        private bool CheckAmount()
        {
            return !string.IsNullOrEmpty(AmoutEntry.Text) && AmoutEntry.Text != "." && !AmoutEntry.Text.Contains("-");
        }

    }
}