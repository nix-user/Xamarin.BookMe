using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.Abstract
{
    /// <summary>
    /// Defines specific methods for working <see cref="Reservation"/>
    /// </summary>
    public interface IReservationRepository : IRepository<Reservation>
    {
        /// <summary>
        /// A method to receive all the grouped reservations of a current user
        /// </summary>
        /// <returns>An operation result with grouped reservations</returns>
        Task<BaseOperationResult<UserReservationsModel>> GetUserReservations();
    }
}