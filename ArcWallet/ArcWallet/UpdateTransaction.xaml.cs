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
                    ID = transaction.ID,
                    Type = transactionType,
                    Name = nameEntry.Text,
                    Category = categorySelected,
                    Date = dateEntry.Date.ToString(),
                    Amount = float.Parse(AmoutEntry.Text),

                });
                await Navigation.PushAsync(new TabbedMyAccount());
             
                DependencyService.Get<IMessage>().LongAlert("Transaction modifiée avec succès");
            }
            else
            {
                Console.WriteLine("Pas Ok");
                DependencyService.Get<IMessage>().ShortAlert("Entrée non valide");
            }

        }

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