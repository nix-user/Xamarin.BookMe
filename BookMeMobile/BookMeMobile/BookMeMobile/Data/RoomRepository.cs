using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data
{
    /// <summary>
    /// Class provides logic for getting <see cref="Room"/> from data source
    /// </summary>
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly CultureInfo dateTimeCultureInfo = new CultureInfo("en-US");

        public RoomRepository(IHttpService httpService) : base(httpService)
        {
        }

        public override async Task<BaseOperationResult<IEnumerable<Room>>> GetAll()
        {
            return await this.HttpService.Get<IEnumerable<Room>>(RestURl.RoomURl);
        }

        public override async Task<BaseOperationResult<Room>> GetById(int id)
        {
            return await this.HttpService.Get<Room>(RestURl.RoomURl + id);
        }

        public override Task<BaseOperationResult> Add(Room reservation)
        {
            throw new NotImplementedException();
        }

        public override Task<BaseOperationResult> Remove(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for getting empty rooms which satisfied <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">search parameters</param>
        /// <returns>operation result with status code and room collection</returns>
        public async Task<BaseOperationResult<IEnumerable<Room>>> GetEmptyRoom(RoomFilterParameters filter)
        {
            var root = string.Format(
                RestURl.GetEmptyRoom,
                filter.From.ToUniversalTime().ToString(this.dateTimeCultureInfo),
                filter.To.ToUniversalTime().ToString(this.dateTimeCultureInfo),
                filter.HasPolycom,
                filter.IsLarge);
            return await this.HttpService.Get<IEnumerable<Room>>(root);
        }

        /// <summary>
        /// Method for getting active reservation which satisfied <paramref name="reservationsModel"/>
        /// </summary>
        /// <param name="reservationsModel">contains room details</param>
        /// <returns>operation result with status code and room reservation</returns>
        public async Task<BaseOperationResult<IEnumerable<Reservation>>> GetCurrentRoomReservation(RoomReservationsRequestModel reservationsModel)
        {
            var root = string.Format(
                RestURl.GetCurrentRoomReservation,
                reservationsModel.From.ToUniversalTime().ToString(this.dateTimeCultureInfo),
                reservationsModel.To.ToUniversalTime().ToString(this.dateTimeCultureInfo),
                reservationsModel.RoomId);
            return await this.HttpService.Get<IEnumerable<Reservation>>(root);
        }
    }
}