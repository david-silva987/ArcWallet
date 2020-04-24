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
    public partial class TabbedMyAccount : TabbedPage
    {
        public TabbedMyAccount()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            Children.Add(new MyAccount());
            Children.Add(new PageGraphes());
            Children.Add(new PageStatistiques());
            CurrentPage = Children[0];
            this.CurrentPageChanged += PageChanged;
        }
       




        void PageChanged(object sender, EventArgs args)
        {
            var currentPage = CurrentPage as PageGraphes;
            currentPage?.UpdateView();
        }
    }
}