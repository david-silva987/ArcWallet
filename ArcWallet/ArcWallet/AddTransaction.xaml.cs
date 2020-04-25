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

        async void addTransactionButton(object sender, EventArgs e)
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
                Console.WriteLine("OKKKK");
                Console.WriteLine(categorySelected);

                float spentLastWeek = float.Parse(await App.Database.GetSpentLastWeek());
                float budget = await App.Database.GetBudget();
                Console.WriteLine("TOTAL 7 JOURS : " + spentLastWeek);
                if(spentLastWeek + float.Parse(AmoutEntry.Text) > budget && budget != 0 && transactionType == false)
                {
                    string BudgetCheck = await DisplayActionSheet("Budget dépassé. Souhaitez-vous tout de même poursuivre la transaction?", "Oui", "Non");
                    
                    if(BudgetCheck == "Oui")
                    {
                        await App.Database.SaveTransactionAsycn(new Transaction
                        {
                            Type = transactionType,
                            Name = nameEntry.Text,
                            Category = categorySelected,
                            Date = dateEntry.Date.ToString(),
                            Amount = float.Parse(AmoutEntry.Text),

                        });
                        await Navigation.PushAsync(new TabbedMyAccount());
                    }


                }
                else
                {
                    await App.Database.SaveTransactionAsycn(new Transaction
                    {
                        Type = transactionType,
                        Name = nameEntry.Text,
                        Category = categorySelected,
                        Date = dateEntry.Date.ToString(),
                        Amount = float.Parse(AmoutEntry.Text),

                    });
                    await Navigation.PushAsync(new TabbedMyAccount());
                }

                
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