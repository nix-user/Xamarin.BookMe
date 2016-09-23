using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class ReservationsListViewModel : BaseViewModel
    {
        private readonly IReservationService reservationService;

        private ObservableCollection<ReservationViewModel> reservations;

        public Func<ReservationViewModel, Task<bool>> ShowRemoveConfirmationDialog { get; set; }

        public ObservableCollection<ReservationViewModel> Reservations
        {
            get { return this.reservations; }
            protected set
            {
                this.reservations = value;
                this.OnPropertyChanged();
            }
        }

        public ReservationsListViewModel(IEnumerable<Reservation> reservations, bool isToday = false)
        {
            this.reservationService = new ReservationService();

            var reservitionViewModelList = reservations.Select(reservation => new ReservationViewModel(reservation, this));
            this.Reservations = new ObservableCollection<ReservationViewModel>(reservitionViewModelList);
            this.IsToday = isToday;
            this.RemoveReservationCommand = new Command<ReservationViewModel>(this.RemoveReservation);
        }

        public bool IsToday { get; protected set; }

        public ICommand RemoveReservationCommand { get; protected set; }

        private async void RemoveReservation(ReservationViewModel reservation)
        {
            var isConfirmed = await this.ShowRemoveConfirmationDialog(reservation);
            if (isConfirmed)
            {
                var operationResult = await this.reservationService.RemoveReservation(reservation.Id);
                if (operationResult.Status == StatusCode.Ok)
                {
                    var reservationToRemove = this.Reservations.FirstOrDefault(x => x.Id == reservation.Id);
                    this.Reservations.Remove(reservationToRemove);
                }
                else
                {
                    this.ShowErrorMessage(operationResult.Status);
                }
            }
        }
    }
}