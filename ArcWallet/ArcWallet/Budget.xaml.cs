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
    public partial class Budget : ContentPage
    {
        public Budget()
        {
            InitializeComponent();
        }

        async void addBudgetButton(object sender, EventArgs e)
        {
            if(CheckEntryValid())
            {
                if(await App.Database.GetBudget() == 0.0)
                {
                    await App.Database.SaveBudgetAsync(new MyBudget
                    {
                        Amount = float.Parse(BudgetEntry.Text)
                    });
                    await Navigation.PushAsync(new TabbedMyAccount());
                }
                else
                {
                    await App.Database.UpdateBudget(new MyBudget
                {
                    ID = 1,
                    Amount = float.Parse(BudgetEntry.Text),

                });
                await Navigation.PushAsync(new TabbedMyAccount());
                }

            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Entrée non valide");
            }
            
        }

        private bool CheckEntryValid()
        {
            return !string.IsNullOrEmpty(BudgetEntry.Text) && BudgetEntry.Text != "." && !BudgetEntry.Text.Contains("-");
        }
    }
}