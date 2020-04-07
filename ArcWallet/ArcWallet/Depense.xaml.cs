
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Depense : ContentPage
    {
        public Depense()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetUserAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text))
            {
                await App.Database.SavePersonAsync(new User
                {
                    Name = nameEntry.Text,
                    Age = int.Parse(ageEntry.Text),
                    Date = DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")
                });

                nameEntry.Text = ageEntry.Text = string.Empty;
                listView.ItemsSource = await App.Database.GetUserAsync();
            }
        }


        async void OnButtonAdd(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text))
            {
                await App.Database.SavePersonAsync(new User
                {
                    Name = nameEntry.Text,
                    Age = int.Parse(ageEntry.Text),
                    Date = DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")
                });

                nameEntry.Text = ageEntry.Text = string.Empty;
                listView.ItemsSource = await App.Database.GetUserAsync();
            }
        }

    }
}