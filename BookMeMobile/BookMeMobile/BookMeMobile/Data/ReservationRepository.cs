using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Provider;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data
{
    /// <summary>
    /// Class provides logic for getting <see cref="Reservation"/> from data source
    /// </summary>
    public class ReservationRepository : BaseRepository<Reservation>
    {
        public ReservationRepository(IHttpService httpService) : base(httpService)
        {
        }

        public override async Task<BaseOperationResult> Remove(int id)
        {
            return (await this.HttpService.Delete(string.Format(RestURl.BookURI + "{0}", id)));
        }

        public override async Task<BaseOperationResult<IEnumerable<Reservation>>> GetAll()
        {
            return await this.HttpService.Get<IEnumerable<Reservation>>(RestURl.GetUserReservation);
        }

        public override Task<BaseOperationResult<Reservation>> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<BaseOperationResult> Add(Reservation reservation)
        {
            return await this.HttpService.Post(RestURl.BookURI, reservation);
        }
    }
}