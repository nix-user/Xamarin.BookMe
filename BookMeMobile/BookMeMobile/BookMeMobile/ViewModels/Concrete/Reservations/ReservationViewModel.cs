using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class ReservationViewModel : BaseViewModel
    {
        private readonly Reservation reservation;

        public ReservationViewModel(Reservation reservation, ReservationsListViewModel parent)
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