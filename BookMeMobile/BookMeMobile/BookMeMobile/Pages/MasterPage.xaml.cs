using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
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
        private ZXingScannerPage scanPage;

        public MasterPage(Page page)
        {
            var viewModel = new MasterViewModel(new NavigationService(this.Navigation), page);
            this.Master = viewModel.MasterPage;
            this.Detail = viewModel.DetailPage;
            this.IsPresented = viewModel.IsPresented;
        }
    }
}