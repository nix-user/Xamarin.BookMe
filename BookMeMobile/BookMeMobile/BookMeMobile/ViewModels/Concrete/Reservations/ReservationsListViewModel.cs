using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class ReservationsListViewModel : BaseViewModel
    {
        private readonly IReservationService reservationService;

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

        public ReservationsListViewModel(
            INavigationService navigationService,
            MyReservationsViewModel parent,
            IEnumerable<Reservation> reservations,
            bool isToday = false) : base(navigationService)
        {
            var reservitionViewModelList = reservations.Select(reservation => new ReservationViewModel(this.NavigationService, reservation, this));
            this.Parent = parent;
            this.Reservations = new ObservableCollection<ReservationViewModel>(reservitionViewModelList);
            this.IsToday = isToday;
        }

        public void RemoveReservationIfExist(int reservationId)
        {
            var reservationToRemove = this.Reservations.FirstOrDefault(x => x.Id == reservationId);
            this.Reservations.Remove(reservationToRemove);
        }

        public bool IsToday { get; protected set; }
    }
}