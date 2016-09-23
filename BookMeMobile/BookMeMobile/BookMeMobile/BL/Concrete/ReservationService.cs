using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Concrete
{
    /// <summary>
    /// This class performs a logic for managing reservations
    /// </summary>
    internal class ReservationService : BaseService, IReservationService
    {
        private readonly IReservationRepository reservationsRepository;

        public ReservationService(IReservationRepository reservationsRepository)
        {
            this.reservationsRepository = reservationsRepository;
        }

        public async Task<BaseOperationResult<UserReservationsModel>> GetUserReservations()
        {
            return await this.reservationsRepository.GetUserReservations();
        }

        public async Task<BaseOperationResult> RemoveReservation(int id)
        {
            return await this.reservationsRepository.Remove(id);
        }
    }
}