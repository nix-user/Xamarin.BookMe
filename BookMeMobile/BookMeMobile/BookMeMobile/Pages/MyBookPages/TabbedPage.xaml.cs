using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyBookPages
{
    public partial class TabPanelPage : TabbedPage
    {
        public TabPanelPage()
        {
            this.InitializeComponent();
            //this.Children.Add(new MyBooks(currentUser));
            //this.Children.Add(new RecursiveBookPage());
        }
    }
}