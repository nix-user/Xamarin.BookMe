using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data
{
    public class RoomRepository : BaseRepository
    {
        private readonly CultureInfo dateTimeCultureInfo = new CultureInfo("en-US");

        public async Task<BaseOperationResult<IEnumerable<Room>>> GetAllRoom()
        {
            return await this.HttpService.Get<IEnumerable<Room>>(RestURl.RoomURl);
        }

        public async Task<BaseOperationResult<Room>> GetRoom(int id)
        {
            return await this.HttpService.Get<Room>(RestURl.RoomURl + id);
        }

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