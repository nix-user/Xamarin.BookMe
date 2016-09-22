using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class MyReservationsViewModel : BaseViewModel
    {
        private readonly IReservationService reservationService;

        public MyReservationsViewModel()
        {
            this.reservationService = new ReservationService();
            var a = this.reservationService.GetUserReservations().ContinueWith(x =>
            {
                var b = x.Result;
            });
        }
    }
}