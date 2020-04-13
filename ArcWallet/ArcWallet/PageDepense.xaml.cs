
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace ArcWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDepense : ContentPage
    {


        public PageDepense()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetExpenditureAsync();

        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("Action", "Que souhaitez-vous faire?", "Modifier", "Supprimer"); //true ->modifier ,false ->supprimer
            var content = e.Item as Expenditure;
            Console.WriteLine(content.ID);
            if(!answer)
            {
                await App.Database.RemoveExpenditure(content.ID);
                listView.ItemsSource = await App.Database.GetExpenditureAsync();
            }
            else
            {
                await Navigation.PushAsync(new ModifierMouvement(content));

            }
        } 




    }
}