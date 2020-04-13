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
    public partial class ModifierMouvement : ContentPage
    {
        Expenditure expenditure = new Expenditure();
        Revenue revenue = new Revenue();
        public ModifierMouvement()
        {
            InitializeComponent();
        }

        public void mouvementEntry_SelectedIndexChanged(object sender, EventArgs e)
        {

            var picker = sender as Picker;
            if (picker.Items[picker.SelectedIndex].Equals("Dépense"))
            {
                categoryEntry.IsVisible = true;
                labelCategory.IsVisible = true;
            }
            else
            {
                categoryEntry.IsVisible = false;
                labelCategory.IsVisible = false;
            }
        }

        public ModifierMouvement(Expenditure mouvement) //flag : 1->depense,2->revenu
        {

            this.expenditure = mouvement;

            Console.WriteLine();
            InitializeComponent();
            mouvementEntry.IsEnabled = false;
            labelMouvementEntry.IsEnabled = false;
            mouvementEntry.SelectedIndex = 0;
            nameEntry.Text = expenditure.Name;
            dateEntry.Date = DateTime.Parse(expenditure.Date);
            AmoutEntry.Text = expenditure.Amount.ToString();
            
            if(expenditure.Category.Equals("Permanent"))
            {
                categoryEntry.SelectedIndex = 0;
            }
            else
            {
                categoryEntry.SelectedIndex = 1;

            }
        }

        public ModifierMouvement(Revenue mouvement)
        {

            this.revenue = mouvement;

            Console.WriteLine();
            InitializeComponent();
            mouvementEntry.IsEnabled = false;
            labelMouvementEntry.IsEnabled = false;
            mouvementEntry.SelectedIndex = 1;

            categoryEntry.IsVisible = false;
            labelCategory.IsVisible = false;

            nameEntry.Text = revenue.Name;
            dateEntry.Date = DateTime.Parse(revenue.Date);
            AmoutEntry.Text = revenue.Amount.ToString();



        }

        async void modifyMouvementButton(object sender, EventArgs e)
        {
            Console.WriteLine(expenditure.Name);
            Console.WriteLine(expenditure.Date);
            Console.WriteLine(expenditure.Amount);
            Console.WriteLine(expenditure.Category);

            Console.WriteLine(nameEntry.Text);
            Console.WriteLine(categoryEntry.SelectedItem);
            Console.WriteLine(dateEntry.Date);
            Console.WriteLine(AmoutEntry.Text);
            Console.ReadLine();
            /*await App.Database.UpdateExpenditure(nameEntry.Text,
                        categoryEntry.SelectedItem.ToString(),
                        dateEntry.Date.ToString(),
                        float.Parse(AmoutEntry.Text),
                        expenditure.Name,
                        expenditure.Category,
                        expenditure.Date,
                        expenditure.Amount);*/
            if (mouvementEntry.SelectedItem.ToString().Equals("Dépense"))
            {
                await App.Database.UpdateExpenditure(new Expenditure
                {
                    ID = expenditure.ID,
                    Name = nameEntry.Text,
                    Category = categoryEntry.SelectedItem.ToString(),
                    Date = dateEntry.Date.ToString(),
                    Amount = float.Parse(AmoutEntry.Text),

                });
            }
            else
            {
                await App.Database.UpdateRevenue(new Revenue
                {
                    ID = revenue.ID,
                    Name = nameEntry.Text,
                    Date = dateEntry.Date.ToString(),
                    Amount = float.Parse(AmoutEntry.Text),

                });
            }
              

            await Navigation.PushAsync(new TabbedMyAccount());



        }
    }
}