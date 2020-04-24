using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcWallet
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {

        public List<MainMenuItem> menuList { get; set; }

        public MainPage()
        {
            InitializeComponent();

            menuList = new List<MainMenuItem>();

            var pageMyAccount = new MainMenuItem() { Title = "Mon compte", Icon = "wallet.png", TargetType = typeof(TabbedMyAccount) };
            var pageBudget = new MainMenuItem() { Title = "Définir mon budget", Icon = "budget.png", TargetType = typeof(Budget) };
            var pageAbout = new MainMenuItem() { Title = "A Propos", Icon = "info.png", TargetType = typeof(About) };
            //var pageDepense = new MainMenuItem() { Title = "Ajouter dépense", Icon = "money_out.png", TargetType = typeof(addExpenditure) };
            //var pageRevenu = new MainMenuItem() { Title = "Ajouter revenu", Icon = "money_in.png", TargetType = typeof(AjouterRevenu) };

            //need to change the icons!

            menuList.Add(pageMyAccount);
            menuList.Add(pageBudget);
            menuList.Add(pageAbout);
            // menuList.Add(pageDepense);
            //menuList.Add(pageRevenu);

            navigationDrawerList.ItemsSource = menuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(TabbedMyAccount)));



        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MainMenuItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
       

    }
}
