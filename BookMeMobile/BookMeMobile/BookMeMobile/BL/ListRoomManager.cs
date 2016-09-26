using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels.Concrete;
using Org.Apache.Http.Impl.Cookie;

namespace BookMeMobile.BL
{
    /// <summary>
    /// Class provides ability to manage room list
    /// </summary>
    public class ListRoomManager
    {
        private readonly IRepository<Room> roomRepository;
        private readonly IRepository<Reservation> reservationRepository;

        public ListRoomManager()
        {
            this.reservationRepository = new ReservationRepository(new HttpService(new CustomDependencyService(), new HttpClientHandler()));
            this.roomRepository = new RoomRepository(new HttpService(new CustomDependencyService(), new HttpClientHandler()));
        }

        /// <summary>
        /// Add <paramref name="model"/> to reservations
        /// </summary>
        /// <param name="model">Reservation for adding</param>
        /// <returns>operation result</returns>
        public async Task<BaseOperationResult> AddReservation(AddReservationModel model)
        {
            Reservation reservation = new Reservation()
            {
                From = model.Date.Date.Add(model.From.TimeOfDay),
                To = model.Date.Date.Add(model.To.TimeOfDay),
                Duration = model.Duration,
                IsRecursive = model.IsRecursive,
                Title = model.Title,
                ResourceId = model.ResourceId
            };
            return (await this.reservationRepository.Add(reservation));
        }

        /// <summary>
        /// Delete reservation <paramref name="idReservation"/>
        /// </summary>
        /// <param name="idReservation">identifier of reservation</param>
        /// <returns>operation result</returns>
        public async Task<BaseOperationResult> DeleteReservation(int idReservation)
        {
            return await this.reservationRepository.Remove(idReservation);
        }

        /// <summary>
        /// Sort room collection by user preference
        /// </summary>
        /// <param name="list">Collection to sort</param>
        /// <returns></returns>
        public List<RoomViewModel> Sort(List<RoomViewModel> list)
        {
            User currentUser = new User();
            currentUser.MyRoom = "410";
            currentUser.FavoriteRoom = "505";
            int userFloor = this.GetFloorInNumber(currentUser.MyRoom);
            list.Sort((view1, view2) =>
            {
                if (Math.Abs(GetFloorInNumber(view1.NumberRoom.ToString()) - userFloor) >
                    Math.Abs(GetFloorInNumber(view2.NumberRoom.ToString()) - userFloor))
                {
                    return 1;
                }
                else
                {
                    if (Math.Abs(GetFloorInNumber(view1.NumberRoom.ToString()) - userFloor) <
                        Math.Abs(GetFloorInNumber(view2.NumberRoom.ToString()) - userFloor))
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            });
            if (list.FindIndex(x => x.NumberRoom == currentUser.FavoriteRoom) > 0)
            {
                RoomViewModel first = list[list.FindIndex(x => x.NumberRoom == currentUser.FavoriteRoom)];
                list.Remove(first);
                list.Insert(0, first);
            }

            return list;
        }

        /// <summary>
        /// Method extracts floor number from room title
        /// </summary>
        /// <param name="roomTitle">Title of room</param>
        /// <returns>floor number</returns>
        public int GetFloorInNumber(string roomTitle)
        {
            return roomTitle[0];
        }

        /// <summary>
        /// Method search empty rooms related to <paramref name="filter"/>
        /// </summary>
        /// <param name="model">Search filter</param>
        /// <returns>operation result with <see cref="Room"/> collection</returns>
        public async Task<BaseOperationResult<IEnumerable<Room>>> GetEmptyRoom(SelectModel model)
        {
            RoomFilterParameters filter = new RoomFilterParameters()
            {
                From = model.Date.Date.Add(model.From.TimeOfDay),
                To = model.Date.Date.Add(model.To.TimeOfDay),
                IsLarge = model.IsLarge,
                HasPolycom = model.HasPolycom
            };
            return await(this.roomRepository as IRoomRepository)?.GetEmptyRoom(filter);
        }

        /// <summary>
        /// Method for search reservation of specified by <paramref name="number"/> room
        /// </summary>
        /// <param name="roomId">identifier of room</param>
        /// <returns>operation result with <see cref="Reservation"/> collection</returns>
        public async Task<BaseOperationResult<IEnumerable<Reservation>>> GetRoomCurrentReservations(string roomId)
        {
            var reservationCurrent = new RoomReservationsRequestModel()
            {
                From = DateTime.Now,
                To = DateTime.Now.AddHours(1),
                RoomId = int.Parse(roomId)
            };
            return await(this.roomRepository as IRoomRepository)?.GetCurrentRoomReservation(reservationCurrent);
        }

        /// <summary>
        /// Method adds one-hour reservation for room specified by <paramref name="roomId"/>
        /// </summary>
        /// <param name="roomId">identifier of room</param>
        /// <returns>operation result</returns>
        public async Task<BaseOperationResult> AddReservationInHour(string roomId)
        {
            Reservation reservation = new Reservation()
            {
                From = DateTime.Now,
                To = DateTime.Now.AddHours(1),
                ResourceId = int.Parse(roomId),
                Duration = new TimeSpan(1, 0, 0)
            };
            return await this.reservationRepository.Add(reservation);
        }
    }
}