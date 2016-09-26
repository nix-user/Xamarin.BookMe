using System.Threading.Tasks;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.ViewModels.Concrete.Reservations;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyReservations
{
    public partial class MyReservationsPage : TabbedPage
    {
        public MyReservationsPage()
        {
            this.InitializeComponent();
            var viewModel = new MyReservationsViewModel(new NavigationService(this.Navigation));
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
            const string CancelActionTitle = "Отмена";
            const string OkActionTitle = "Да";

            var actionSheetMessage = reservationViewModel.TextPeriod + " " + reservationViewModel.Title;

            return await this.DisplayAlert(ConfirmationHeader, ConfirmationMessage + actionSheetMessage, OkActionTitle, CancelActionTitle);
        }
    }
}