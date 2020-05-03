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
    /// Class that creates a TabbedPage
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMyAccount : TabbedPage
    {
        protected override bool OnBackButtonPressed() => false; //disable back button on android device

        public TabbedMyAccount()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();

            //tabbed page made of 3 pages
            Children.Add(new MyAccount());
            Children.Add(new PageGraphes());
            Children.Add(new PageStatistiques());
            CurrentPage = Children[0]; //the default page is MyAccount
            this.CurrentPageChanged += PageChanged;
        }
        /// <summary>
        /// Keep a track in active page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void PageChanged(object sender, EventArgs args)
        {
            var currentPage = CurrentPage as PageGraphes;
            currentPage?.UpdateView();
        }

        
   
    }
}