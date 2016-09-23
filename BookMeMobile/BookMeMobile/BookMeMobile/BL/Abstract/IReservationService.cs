using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Abstract
{
    internal interface IReservationService
    {
        Task<BaseOperationResult<UserReservationsModel>> GetUserReservations();

        Task<BaseOperationResult> RemoveReservation(int id);
    }
}