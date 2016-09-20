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
using Org.Apache.Http.Impl.Cookie;

namespace BookMeMobile.BL
{
    public class ListRoomManager
    {
        private static int counter = 0;

        private RoomRepository roomRepository;
        private ReservationRepository reservationRepository;

        private ReservationModel currentReservation;

        public ListRoomManager(ReservationModel reservation, User currentUser)
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

        public async Task<OperationResult> AddReservation(RoomFilterParameters reservation, int idRoom)
        {
            return (await this.reservationRepository.AddReservation(reservation, idRoom));
        }

        public async Task<OperationResult> DeleteReservation(int idReservation)
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

        public async Task<OperationResult<IEnumerable<Room>>> GetEmptyRoom(RoomFilterParameters filter)
        {
            return await this.roomRepository.GetEmptyRoom(filter);
        }

        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetAllUserReservation()
        {
            return await this.reservationRepository.GetUserReservations();
        }

        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetRoomCurrentReservations(string number)
        {
            var reservationCurrent = new RoomReservationsRequestModel()
            {
                From = DateTime.Now,
                To = DateTime.Now.AddHours(1),
                RoomId = int.Parse(number)
            };
            return await this.roomRepository.GetCurrentRoomReservation(reservationCurrent);
        }

        public async Task<OperationResult> AddReservationInHour(string text)
        {
           RoomFilterParameters parametr  = new RoomFilterParameters()
            {
               From = DateTime.Now,
               To = DateTime.Now.AddHours(1),
            };
            return await this.reservationRepository.AddReservation(parametr, int.Parse(text));
        }
    }
}