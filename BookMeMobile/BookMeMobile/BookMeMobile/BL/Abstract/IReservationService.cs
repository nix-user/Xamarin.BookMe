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
        /// <summary>
        /// A method to recieve all the grouped reservations of the current user 
        /// </summary>
        /// <returns>An operation result with grouped reservations</returns>
        Task<BaseOperationResult<UserReservationsModel>> GetUserReservations();

        /// <summary>
        /// A method to remove a reservation
        /// </summary>
        /// <param name="id">Id of a reservation</param>
        /// <returns>An operation result</returns>
        Task<BaseOperationResult> RemoveReservation(int id);
    }
}