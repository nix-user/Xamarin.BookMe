using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage(Page page, NavigationService navigationService)
        {
            this.Detail = page;
            var menu = new MenuPage(navigationService);
            menu.ViewModel = new MenuPageViewModel(navigationService);
            this.Master = menu;
        }
    }
}