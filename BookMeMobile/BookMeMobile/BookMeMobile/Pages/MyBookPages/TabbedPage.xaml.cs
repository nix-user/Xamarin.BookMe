using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyBookPages
{
    public partial class TabPanelPage : TabbedPage
    {
        public TabPanelPage(User currentUser)
        {
            this.InitializeComponent();
            this.Children.Add(new AllMyBook(currentUser));
            this.Children.Add(new RecursiveBookPage(currentUser));
        }
    }
}