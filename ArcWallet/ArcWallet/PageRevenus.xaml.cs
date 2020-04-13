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
    public partial class PageRevenus : ContentPage
    {
        public PageRevenus()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetRevenueAsync();
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("Action", "Que souhaitez-vous faire?", "Modifier", "Supprimer"); //true ->modifier ,false ->supprimer
            var content = e.Item as Revenue;
            Console.WriteLine(content.ID);
            if (!answer)
            {
                await App.Database.RemoveRevenu(content.ID);
                listView.ItemsSource = await App.Database.GetRevenueAsync();
            }
        }
    }
}