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
    /// Class to set a budget weekly or monthly for user
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBudget : ContentPage
    {
        public MyBudget()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Action onClick Button to add budget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void addBudgetButton(object sender, EventArgs e)
        {
            bool budgetType; //to match type on database
            //check if form is valid
            if (CheckEntryValid())
            {
                if (budgetPicker.SelectedItem.ToString().Equals("Hebdomadaire"))
                {
                    budgetType = true;
                }
                else
                {
                    budgetType = false;
                }

                if (await App.Database.GetBudget() == 0.0)
                {

                    await App.Database.SaveBudgetAsync(new Budget
                    {
                        Type= budgetType,
                        Date= DateTime.Now.ToString(),
                        Amount = float.Parse(BudgetEntry.Text)

                    });
                    await Navigation.PushAsync(new TabbedMyAccount());
                }
                else
                {
                    //if we update, since we have only 1 row in table, use ID=1 to update that row
                    await App.Database.UpdateBudget(new Budget
                {
                    ID = 1,
                    Type = budgetType,
                    Date = DateTime.Now.ToString(),
                    Amount = float.Parse(BudgetEntry.Text)

                    });

                    //a few debugs
                    Console.WriteLine("Type:" + budgetType);
                    Console.WriteLine("Date:" + DateTime.Now.ToString());
                    Console.WriteLine("Amount:" + float.Parse(BudgetEntry.Text));
                await Navigation.PushAsync(new TabbedMyAccount());
                }

            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Entrée non valide"); //form is not valid
            }
            
        }

        /// <summary>
        /// Check if form is valid while checking if a Budget was set
        /// </summary>
        /// <returns></returns>
        private bool CheckEntryValid()
        {
            return !string.IsNullOrEmpty(BudgetEntry.Text) && BudgetEntry.Text != "." && !BudgetEntry.Text.Contains("-");
        }
    }
}