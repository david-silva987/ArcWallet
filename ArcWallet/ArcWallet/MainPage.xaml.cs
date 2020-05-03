using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcWallet
{
    /// <summary>
    /// Navbar class
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {

        public List<MainMenuItem> menuList { get; set; }
        protected override bool OnBackButtonPressed() => false;

        public MainPage()
        {
            InitializeComponent();

            menuList = new List<MainMenuItem>();

            //list of all pages in the navbar
            var pageMyAccount = new MainMenuItem() { Title = "Mon compte", Icon = "wallet.png", TargetType = typeof(TabbedMyAccount) };
            var pageMyBudget = new MainMenuItem() { Title = "Définir mon budget", Icon = "budget.png", TargetType = typeof(MyBudget) };
            var pageAbout = new MainMenuItem() { Title = "A Propos", Icon = "info.png", TargetType = typeof(About) };

            menuList.Add(pageMyAccount);
            menuList.Add(pageMyBudget);
            menuList.Add(pageAbout);

            navigationDrawerList.ItemsSource = menuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(TabbedMyAccount)));



        }

        /// <summary>
        /// Change page if a page is clicked in the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MainMenuItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
       

    }
}
