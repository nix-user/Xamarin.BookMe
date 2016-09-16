using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Newtonsoft.Json;

namespace BookMeMobile.Data
{
    public class RoomRepository
    {
        private readonly HttpService httpService;

        public RoomRepository()
        {
            this.httpService = new HttpService();
        }

        public async Task<OperationResult<IEnumerable<Room>>> GetAllRoom()
        {
            return await this.httpService.Get<IEnumerable<Room>>(RestURl.RoomURl);
        }

        public async Task<OperationResult<Room>> GetRoom(int id)
        {
            return await this.httpService.Get<Room>(RestURl.RoomURl + id);
        }

        public async Task<OperationResult<IEnumerable<Room>>> GetEmptyRoom(RoomFilterParameters filter)
        {
            return await this.httpService.Post<RoomFilterParameters, IEnumerable<Room>>(RestURl.GetEmptyRoom, filter);
        }

        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetCurrentRoomReservation(RoomReservationsRequestModel reservationsModel)
        {
            return await this.httpService.Post<RoomReservationsRequestModel, IEnumerable<ReservationModel>>(RestURl.GetCurrentRoomReservation, reservationsModel);
        }
    }
}