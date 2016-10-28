using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.FakeRepository
{
   public class FakeReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public FakeReservationRepository(IHttpService httpService) : base(httpService)
        {
        }

        public override Task<BaseOperationResult> Add(Reservation reservation)
        {
            return Task.FromResult(FakeData.SuccessResult());
        }

        public override Task<BaseOperationResult<IEnumerable<Reservation>>> GetAll()
        {
            return Task.FromResult(FakeData.ReservationList);
        }

        public override Task<BaseOperationResult<Reservation>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseOperationResult<UserReservationsModel>> GetUserReservations()
        {
            return Task.FromResult(FakeData.UserReservation);
        }

        public override Task<BaseOperationResult> Remove(int id)
        {
            return Task.FromResult(FakeData.SuccessResult());
        }
    }
}