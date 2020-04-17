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
                  
                }
                 else
                    {
                     
                    }


                    nameEntry.Text = AmoutEntry.Text = string.Empty;
                    await Navigation.PushAsync(new TabbedMyAccount());

                
            }
        }

    }
}