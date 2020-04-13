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
        object mouvement = null;
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

        public ModifierMouvement(object mouvement,int flag) //flag : 1->depense,2->revenu
        {
            if(flag==1)
            {
                this.mouvement = mouvement;
                Console.WriteLine();

            }
            InitializeComponent();
        }

        async void modifyMouvementButton(object sender, EventArgs e)
        {

        }
    }
}