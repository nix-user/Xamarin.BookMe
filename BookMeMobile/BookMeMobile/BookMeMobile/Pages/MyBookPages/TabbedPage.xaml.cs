using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyBookPages
{
    public partial class TabPanelPage : TabbedPage
    {
        public TabPanelPage(User currentUser, IEnumerable<Reservation> allReservations)
        {
            this.InitializeComponent();
            List<Reservation> recursive = allReservations.Where(x => x.IsRecursive == true).ToList();
            List<Reservation> noRecursive = allReservations.Where(x => x.IsRecursive == false).ToList();
            Children.Add(new AllMyBook(currentUser, noRecursive, recursive));
            Children.Add(new RecursiveReservationPage(currentUser, recursive));
        }
    }
}