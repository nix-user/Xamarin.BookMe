using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class MyReservationsViewModel : BaseViewModel
    {
        private readonly IReservationService reservationService;

        private ReservationsListViewModel todayReservationsViewModel;
        private ReservationsListViewModel recursiveReservationsViewModel;
        private ReservationsListViewModel allReservationsViewModel;

        public MyReservationsViewModel()
        {
            this.reservationService = new ReservationService();

            this.LoadReservations();
        }

        public ReservationsListViewModel TodayReservationsViewModel
        {
            get
            {
                return this.todayReservationsViewModel;
            }

            protected set
            {
                this.todayReservationsViewModel = value;
                this.OnPropertyChanged();
            }
        }

        public ReservationsListViewModel RecursiveReservationsViewModel
        {
            get
            {
                return this.recursiveReservationsViewModel;
            }

            protected set
            {
                this.recursiveReservationsViewModel = value;
                this.OnPropertyChanged();
            }
        }

        public ReservationsListViewModel AllReservationsViewModel
        {
            get
            {
                return this.allReservationsViewModel;
            }

            protected set
            {
                this.allReservationsViewModel = value;
                this.OnPropertyChanged();
            }
        }

        private async void LoadReservations()
        {
            var reservationsResult = await this.reservationService.GetUserReservations();

            if (reservationsResult.Status == StatusCode.Ok)
            {
                var todayReservations = reservationsResult.Result.TodayReservations;
                // add check if only type 1
                var recursiveReservation = reservationsResult.Result.AllReservations.Where(x => x.IsRecursive);
                var allReservations = reservationsResult.Result.AllReservations;

                this.TodayReservationsViewModel = new ReservationsListViewModel(todayReservations, true);
                this.RecursiveReservationsViewModel = new ReservationsListViewModel(recursiveReservation);
                this.AllReservationsViewModel = new ReservationsListViewModel(allReservations);
            }
            else
            {
                this.ShowErrorMessage(reservationsResult.Status);
            }
        }
    }
}