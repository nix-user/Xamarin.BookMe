using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Abstract
{
    /// <summary>
    /// This interface which performs contract for managing reservations
    /// </summary>
    internal interface IReservationService
    {
        Task<BaseOperationResult<UserReservationsModel>> GetUserReservations();

        Task<BaseOperationResult> RemoveReservation(int id);
    }
}