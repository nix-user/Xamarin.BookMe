using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.Model;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

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