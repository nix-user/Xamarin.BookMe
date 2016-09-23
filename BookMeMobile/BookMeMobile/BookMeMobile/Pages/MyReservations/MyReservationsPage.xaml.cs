using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Entity;
using BookMeMobile.ViewModels.Concrete;
using BookMeMobile.ViewModels.Concrete.Reservations;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyReservations
{
    public partial class MyReservationsPage : TabbedPage
    {
        public MyReservationsPage()
        {
            this.InitializeComponent();
            var viewModel = new MyReservationsViewModel();
            viewModel.ShowInfoMessage += this.ShowInfoMessage;
            this.BindingContext = viewModel;
        }

        private void ShowInfoMessage(string title, string content, string cancelText)
        {
            this.DisplayAlert(title, content, cancelText);
        }
    }
}