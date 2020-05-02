
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;
namespace ArcWallet
{

    /// <summary>
    /// Class to display Graph using Microcharts, second tab in tabbed page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGraphes : ContentPage
    {

        public PageGraphes()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Method called when page is active to user
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //get total spending and receiving
            totalSpentBinding.Text = await App.Database.GetAmountExpenditures();
            totalReceivedBinding.Text = await App.Database.GetAmountRevenues();


            //display some informations and hide another depending on amount received/spent
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

            //List of Entry, used to display Microchart
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

            //Creation of Microcharts Donut Chart using entries given
            Chart1.Chart = new Microcharts.DonutChart { Entries = entries };
            Chart1.Chart.LabelTextSize = 30;
            

        }

        internal void UpdateView()
        {
            OnAppearing();
        }
    }
}