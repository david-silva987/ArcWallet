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

        async void addTransactionButton(object sender, EventArgs e)
        {
            //If form is valid
            if (CheckFormValid())
            {
                float spentLastWeek = float.Parse(await App.Database.GetSpentLastSevenDays()); //return 0 if budget is not defined
                float budget = await App.Database.GetBudget();
                /*Check if:
                 * transactionPicker' selected item is "Dépense"
                 * Date of transaction is not before last 7 days
                 * Budget is not equal to 0 (because 0 -> budget is not defined)
                 * Last 7 Days spent's amount + amount in form  is bigger than budget)
                 */

                if (dateEntry.Date.Date > DateTime.Now.Date.AddDays(-7) && transactionPicker.SelectedItem.ToString().Equals("Dépense") && budget != 0 && spentLastWeek + float.Parse(AmoutEntry.Text) > budget)
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

        //Add transaction to DB
        private async void AddTransactionToDB()
        {
            bool transactionType;
            if (transactionPicker.SelectedItem.ToString().Equals("Dépense"))
            {
                transactionType = false;
            }
            else
            {
                transactionType = true;
            }

            await App.Database.SaveTransactionAsycn(new Transaction
            {
                Type = transactionType,
                Name = nameEntry.Text,
                Category = categoryPicker.SelectedItem.ToString(),
                Date = dateEntry.Date.ToString(),
                Amount = float.Parse(AmoutEntry.Text),

            });
            await Navigation.PushAsync(new TabbedMyAccount());
        }

        //Check if form's entrys are valid
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

        private bool CheckName()
        {
            return !string.IsNullOrEmpty(nameEntry.Text);
        }

        private bool CheckCategory()
        {
            return !string.IsNullOrEmpty(categoryPicker.SelectedItem.ToString());
        }

        private bool CheckAmount()
        {
            return !string.IsNullOrEmpty(AmoutEntry.Text) && AmoutEntry.Text != "." && !AmoutEntry.Text.Contains("-");
        }

    }
}