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
            viewModel.ShowRemoveConfirmationDialog += this.ShowRemoveConfirmationDialog;
            this.BindingContext = viewModel;
        }

        private void ShowInfoMessage(string title, string content, string cancelText)
        {
            this.DisplayAlert(title, content, cancelText);
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