using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.ViewModels.Abstract;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class ReservationViewModel : BaseViewModel
    {
        private readonly Reservation reservation;

        public ReservationViewModel(INavigationService navigationService, Reservation reservation, ReservationsListViewModel parent) : base(navigationService)
        {
            this.reservation = reservation;
            this.Parent = parent;
        }

        public ReservationsListViewModel Parent { get; protected set; }

        public int Id => this.reservation.Id;

        public string Title => this.reservation.Title;

        public string TextPeriod => this.reservation.TextPeriod;

        public string TextRule => this.reservation.TextRule;

        public bool IsRecursive => this.reservation.IsRecursive;

        public string RoomNumber => this.reservation.Room.Number;
    }
}