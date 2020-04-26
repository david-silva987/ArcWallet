
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;
namespace ArcWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGraphes : ContentPage
    {

        
        public PageGraphes()
        {

            InitializeComponent();

            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // listView.ItemsSource = await App.Database.GetExpenditureAsync();
            totalSpentBinding.Text = await App.Database.GetAmountExpenditures();
            totalReceivedBinding.Text = await App.Database.GetAmountRevenues();


            if(float.Parse(totalReceivedBinding.Text) >0 || float.Parse(totalSpentBinding.Text) >0)
            {
                Chart1.IsVisible = true;
                labelNoActivity.IsVisible = false;
            }
            else
            {

                    Chart1.IsVisible = false;
                    labelNoActivity.IsVisible = true;
                
            }

            List<Entry> entries = new List<Entry>()
            {
                new Entry(float.Parse(totalSpentBinding.Text))
                {
                     Label = "Dépenses",
                     ValueLabel = totalSpentBinding.Text,
                     Color = SKColor.Parse("#fad1d0") //light red
                },
                   new Entry(float.Parse(totalReceivedBinding.Text))
                {
                     Label = "Revenu",
                     ValueLabel = totalReceivedBinding.Text,
                     Color = SKColor.Parse("#00fa9a")//light green
                }
            };

            Chart1.Chart = new Microcharts.DonutChart { Entries = entries };
            Chart1.Chart.LabelTextSize = 30;
            

        }

        internal void UpdateView()
        {
            OnAppearing();
        }
    }
}