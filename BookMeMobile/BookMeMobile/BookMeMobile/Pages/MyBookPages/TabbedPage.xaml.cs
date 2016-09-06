using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyBookPages
{
    public partial class TabPanelPage : TabbedPage
    {
        public TabPanelPage(User currentUser, List<MyReservationViewResult> recursive, List<MyReservationViewResult> allBook)
        {
            this.InitializeComponent();
            Children.Add(new AllMyBook(currentUser, allBook, recursive));
            Children.Add(new RecursiveReservationPage(currentUser, recursive));
        }
    }
}