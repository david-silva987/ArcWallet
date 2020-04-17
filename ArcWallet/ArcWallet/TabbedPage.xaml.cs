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
            InitializeComponent();
            Children.Add(new MyAccount());
            Children.Add(new PageDepense());
            Children.Add(new PageRevenus());
            CurrentPage = Children[0];
        }
    }
}