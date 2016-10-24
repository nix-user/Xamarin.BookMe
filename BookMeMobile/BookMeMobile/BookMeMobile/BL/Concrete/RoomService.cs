using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Concrete
{
    /// <summary>
    /// This class performs a logic for managing rooms
    /// </summary>
    public class RoomService : BaseService, IRoomService
    {
        private IRoomRepository roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task<BaseOperationResult<IEnumerable<Room>>> GetAllRoom()
        {
            return await this.roomRepository.GetAll();
        }
    }
}