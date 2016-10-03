using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.FakeRepository
{
   public class FakeRoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public FakeRoomRepository(IHttpService httpService) : base(httpService)
        {
        }

        public override Task<BaseOperationResult> Add(Room reservation)
        {
            throw new NotImplementedException();
        }

        public override Task<BaseOperationResult<IEnumerable<Room>>> GetAll()
        {
            return Task.FromResult(FakeData.ListRoomResult);
        }

        public override Task<BaseOperationResult<Room>> GetById(int id)
        {
            return Task.FromResult(FakeData.Room);
        }

        public Task<BaseOperationResult<IEnumerable<Reservation>>> GetCurrentRoomReservation(RoomReservationsRequestModel reservationsModel)
        {
            return Task.FromResult(FakeData.ReservationList);
        }

        public Task<BaseOperationResult<IEnumerable<Room>>> GetEmptyRoom(RoomFilterParameters filter)
        {
            return Task.FromResult(FakeData.ListRoomResult);
        }

        public override Task<BaseOperationResult> Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}