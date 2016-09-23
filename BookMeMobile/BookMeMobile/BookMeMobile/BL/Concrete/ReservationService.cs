using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Concrete
{
    /// <summary>
    /// This class performs a logic for managing reservations
    /// </summary>
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

        public async Task<BaseOperationResult> RemoveReservation(int id)
        {
            return await this.reservationsRepository.RemoveReservation(id);
        }
    }
}