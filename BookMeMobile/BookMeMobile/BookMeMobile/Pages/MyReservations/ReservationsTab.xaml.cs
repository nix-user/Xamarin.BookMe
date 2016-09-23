using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.ViewModels.Concrete.Reservations;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyReservations
{
    public partial class ReservationsTab : BasePage
    {
        public ReservationsTab()
        {
            this.InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = this.BindingContext as ReservationsListViewModel;
            if (viewModel != null)
            {
                this.SetUpViewModelSubscriptions(viewModel);
            }
        }
    }
}