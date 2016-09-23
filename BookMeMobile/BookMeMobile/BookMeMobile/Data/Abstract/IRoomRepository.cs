using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.Abstract
{
    /// <summary>
    /// Defines specific methods for working <see cref="Room"/>
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Method for getting empty rooms which satisfied <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">search parameters</param>
        /// <returns>operation result with status code and room collection</returns>
        Task<BaseOperationResult<IEnumerable<Room>>> GetEmptyRoom(RoomFilterParameters filter);

        /// <summary>
        /// Method for getting active reservation which satisfied <paramref name="reservationsModel"/>
        /// </summary>
        /// <param name="reservationsModel">contains room details</param>
        /// <returns>operation result with status code and room reservation</returns>
        Task<BaseOperationResult<IEnumerable<Reservation>>> GetCurrentRoomReservation(RoomReservationsRequestModel reservationsModel);
    }
}