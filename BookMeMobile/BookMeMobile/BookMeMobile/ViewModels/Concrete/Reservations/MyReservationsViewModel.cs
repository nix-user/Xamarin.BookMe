using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete.Reservations
{
    public class MyReservationsViewModel : BaseViewModel
    {
        private readonly IReservationService reservationService;
        private readonly IReservationRepository reservationRepository;

        private ReservationsListViewModel todayReservationsViewModel;
        private ReservationsListViewModel recursiveReservationsViewModel;
        private ReservationsListViewModel allReservationsViewModel;

        public ICommand RemoveReservationCommand { get; set; }

        public MyReservationsViewModel()
        {
            this.reservationRepository = new ReservationRepository();
            this.reservationService = new ReservationService(this.reservationRepository);
            this.RemoveReservationCommand = new Command<ReservationViewModel>(this.RemoveReservation);

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
                var recursiveReservation = reservationsResult.Result.AllReservations.Where(x => x.IsRecursive);
                var allReservations = reservationsResult.Result.AllReservations;

                this.TodayReservationsViewModel = new ReservationsListViewModel(this, todayReservations, true);
                this.RecursiveReservationsViewModel = new ReservationsListViewModel(this, recursiveReservation);
                this.AllReservationsViewModel = new ReservationsListViewModel(this, allReservations);
            }
            else
            {
                this.ShowErrorMessage(reservationsResult.Status);
            }
        }

        private async void RemoveReservation(ReservationViewModel reservation)
        {
            var isConfirmed = await this.ShowRemoveConfirmationDialog(reservation);
            if (isConfirmed)
            {
                var operationResult = await this.reservationService.RemoveReservation(reservation.Id);
                if (operationResult.Status == StatusCode.Ok)
                {
                    this.AllReservationsViewModel.RemoveReservationIfExist(reservation.Id);
                    this.RecursiveReservationsViewModel.RemoveReservationIfExist(reservation.Id);
                    this.TodayReservationsViewModel.RemoveReservationIfExist(reservation.Id);
                }
                else
                {
                    this.ShowErrorMessage(operationResult.Status);
                }
            }
        }

        public Func<ReservationViewModel, Task<bool>> ShowRemoveConfirmationDialog { get; set; }
    }
}