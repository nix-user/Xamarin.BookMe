using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Concrete
{
    internal class ReservationService : BaseService, IReservationService
    {
        private readonly ReservationRepository reservationsRepository = new ReservationRepository();

        public ReservationService()
        {
            this.reservationsRepository = new ReservationRepository();
        }

        public async Task<BaseOperationResult<UserReservationsModel>> GetUserReservations()
        {
            return await this.reservationsRepository.GetUserReservations();
        }
    }
}