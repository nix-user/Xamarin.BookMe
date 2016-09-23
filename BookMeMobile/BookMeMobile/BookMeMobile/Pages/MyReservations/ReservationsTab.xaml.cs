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
                viewModel.ShowRemoveConfirmationDialog += this.ShowRemoveConfirmationDialog;
                this.SetUpViewModelSubscriptions(viewModel);
            }
        }

        private async Task<bool> ShowRemoveConfirmationDialog(ReservationViewModel reservationViewModel)
        {
            const string ConfirmationHeader = "Подтверждение";
            const string ConfirmationMessage = "Вы действительно хотите удалить резервацию: ";
            const string RemoveActionTitle = "Удалить";
            const string CancelActionTitle = "Отмена";
            const string OkActionTitle = "Да";

            var actionSheetMessage = reservationViewModel.TextPeriod + " " + reservationViewModel.Title;

            var action = await this.DisplayActionSheet(actionSheetMessage, CancelActionTitle, null, RemoveActionTitle);
            if (action == RemoveActionTitle)
            {
                var isConfirmed = await this.DisplayAlert(ConfirmationHeader, ConfirmationMessage + actionSheetMessage, OkActionTitle, CancelActionTitle);
                return isConfirmed;
            }

            return false;
        }
    }
}