using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class ReservationsListViewModel : BaseViewModel
    {
        private ObservableCollection<ReservationViewModel> reservations;

        public MyReservationsViewModel Parent { get; protected set; }

        public ObservableCollection<ReservationViewModel> Reservations
        {
            get { return this.reservations; }
            protected set
            {
                this.reservations = value;
                this.OnPropertyChanged();
            }
        }

        public ReservationsListViewModel(IEnumerable<Reservation> reservations, MyReservationsViewModel parent, bool isToday = false)
        {
            this.Parent = parent;

            var reservitionViewModelList = reservations.Select(reservation => new ReservationViewModel(reservation, this));

            this.Reservations = new ObservableCollection<ReservationViewModel>(reservitionViewModelList);
            this.IsToday = isToday;
            this.RemoveReservationCommand = new Command<ReservationViewModel>(this.RemoveReservation);
        }

        public bool IsToday { get; protected set; }

        public ICommand RemoveReservationCommand { get; protected set; }

        private void RemoveReservation(ReservationViewModel reservation)
        {
            this.Parent.RemoveReservationAction?.Invoke(reservation);
        }
    }
}