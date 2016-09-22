using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels.Concrete;
using Org.Apache.Http.Impl.Cookie;

namespace BookMeMobile.BL
{
    public class ListRoomManager
    {
        private static int counter = 0;

        private RoomRepository roomRepository;
        private ReservationRepository reservationRepository;

        private Reservation currentReservation;

        public ListRoomManager(Reservation reservation, User currentUser)
        {
            this.reservationRepository = new ReservationRepository();
            this.roomRepository = new RoomRepository();
            this.currentReservation = reservation;
        }

        public ListRoomManager()
        {
            this.reservationRepository = new ReservationRepository();
            this.roomRepository = new RoomRepository();
        }

        public async Task<BaseOperationResult> AddReservation(AddReservationModel model)
        {
            Reservation reservation = new Reservation()
            {
                From = model.From,
                To = model.To,
                Duration = model.Duration,
                IsRecursive = model.IsRecursive,
                Title = model.Title,
                ResourceId = model.ResourceId
            };
            return (await this.reservationRepository.AddReservation(reservation));
        }

        public async Task<BaseOperationResult> DeleteReservation(int idReservation)
        {
            return await this.reservationRepository.RemoveReservation(idReservation);
        }

        public List<Room> Sort(List<Room> list)
        {
            User currentUser = new User();
            currentUser.MyRoom = "410";
            currentUser.FavoriteRoom = "505";
            int userFloor = this.GetFloorInNumber(currentUser.MyRoom);
            list.Sort((view1, view2) =>
            {
                if (Math.Abs(GetFloorInNumber(view1.Number.ToString()) - userFloor) >
                    Math.Abs(GetFloorInNumber(view2.Number.ToString()) - userFloor))
                {
                    return 1;
                }
                else
                {
                    if (Math.Abs(GetFloorInNumber(view1.Number.ToString()) - userFloor) <
                        Math.Abs(GetFloorInNumber(view2.Number.ToString()) - userFloor))
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            });
            if (list.FindIndex(x => x.Number == currentUser.FavoriteRoom) > 0)
            {
                Room first = list[list.FindIndex(x => x.Number == currentUser.FavoriteRoom)];
                list.Remove(first);
                list.Insert(0, first);
            }

            return list;
        }

        public int GetFloorInNumber(string s)
        {
            return s[0];
        }

        public async Task<BaseOperationResult<IEnumerable<Room>>> GetEmptyRoom(SelectModel model)
        {
            var filter = new RoomFilterParameters()
            {
                From = model.From,
                To = model.From,
                IsLarge = model.IsLarge,
                HasPolycom = model.HasPolycom
            };

            return await this.roomRepository.GetEmptyRoom(filter);
        }

        public async Task<BaseOperationResult<IEnumerable<Reservation>>> GetAllUserReservation()
        {
            return await this.reservationRepository.GetUserReservations();
        }

        public async Task<BaseOperationResult<IEnumerable<Reservation>>> GetRoomCurrentReservations(string number)
        {
            var reservationCurrent = new RoomReservationsRequestModel()
            {
                From = DateTime.Now,
                To = DateTime.Now.AddHours(1),
                RoomId = int.Parse(number)
            };
            return await this.roomRepository.GetCurrentRoomReservation(reservationCurrent);
        }

        public async Task<BaseOperationResult> AddReservationInHour(string text)
        {
            Reservation reservation = new Reservation()
            {
                From = DateTime.Now,
                To = DateTime.Now.AddHours(1),
                ResourceId = int.Parse(text),
                Duration = new TimeSpan(1, 0, 0)
            };
            return await this.reservationRepository.AddReservation(reservation);
        }
    }
}