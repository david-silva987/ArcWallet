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
    public partial class UpdateTransaction : ContentPage
    {
        Transaction transaction = new Transaction();

        public bool False { get; }

        public UpdateTransaction()
        {
            InitializeComponent();
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
        }
        
    }
}