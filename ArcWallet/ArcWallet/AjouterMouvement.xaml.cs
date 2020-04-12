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
    public partial class AjouterMouvement : ContentPage
    {
        public AjouterMouvement()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();


        }

        public void mouvementEntry_SelectedIndexChanged(object sender, EventArgs e)
        {

            var picker = sender as Picker;
            if(picker.Items[picker.SelectedIndex].Equals("Dépense"))
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

            async void addMouvementButton(object sender, EventArgs e)
        {
            /*int ordre = 0;
            if (categoryEntry.SelectedItem.ToString().Equals("Permanent"))
            {
                ordre = 1;
            }*/


            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(AmoutEntry.Text))
            {
                Console.WriteLine(mouvementEntry.SelectedItem.ToString());
                Console.ReadLine();
                if (mouvementEntry.SelectedItem.ToString().Equals("Dépense"))
                {
                    await App.Database.SaveExpenditureAsync(new Expenditure
                    {
                        Name = nameEntry.Text,
                        Category = categoryEntry.SelectedItem.ToString(),
                        Date = dateEntry.Date.ToString(),
                        Amount = float.Parse(AmoutEntry.Text),

                    });
                }
                 else
                    {
                        await App.Database.SaveRevenuAsycn(new Revenue
                        {
                            Name = nameEntry.Text,
                            //Permanent = ordre,
                            Date = dateEntry.Date.ToString(),
                            Amount = float.Parse(AmoutEntry.Text),

                        });
                    }


                    nameEntry.Text = AmoutEntry.Text = string.Empty;
                    await Navigation.PushAsync(new TabbedMyAccount());

                
            }
        }

    }
}