using System.Threading.Tasks;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.Model;
using BookMeMobile.ViewModels.Concrete.Reservations;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyReservations
{
    public partial class MyReservationsPage : TabbedPage
    {
        public MyReservationsPage(UserReservationsModel reservationsModel)
        {
            this.InitializeComponent();
            var viewModel = new MyReservationsViewModel(reservationsModel, new NavigationService(this.Navigation));
            viewModel.ShowInfoMessage += this.ShowInfoMessage;
            viewModel.ShowRemoveConfirmationDialog += this.ShowRemoveConfirmationDialog;
            this.BindingContext = viewModel;
        }

        private async Task ShowInfoMessage(string title, string content, string cancelText)
        {
            await this.DisplayAlert(title, content, cancelText);
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