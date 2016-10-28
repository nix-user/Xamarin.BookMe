using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Abstract
{
    /// <summary>
    /// This interface which performs contract for managing rooms
    /// </summary>
    public interface IRoomService
    {
        Task<BaseOperationResult<IEnumerable<Room>>> GetAllRoom();
    }
}