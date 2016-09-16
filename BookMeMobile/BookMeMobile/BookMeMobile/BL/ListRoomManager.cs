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

        public async Task<string> ReservationMessag(int idRoom)
        {
            this.currentReservation.Room = (await this.roomRepository.GetRoom(idRoom)).Result;
            Room currentRoom = (await this.roomRepository.GetRoom(idRoom)).Result;
            return string.Format(
                " Комната: {3}\n Дата: {0}\n Время: {1} - {2}\n Большая:{4} Поликом:{5}",
                this.currentReservation.From.Date.ToString("d"),
                this.currentReservation.From.TimeOfDay.ToString(@"hh\:mm"),
                this.currentReservation.To.TimeOfDay.ToString(@"hh\:mm"),
                currentRoom.Number,
                string.Format("{0:Да;0;Нет}", currentRoom.IsBig.GetHashCode()),
                string.Format("{0:Да;0;Нет}", currentRoom.IsHasPolykom.GetHashCode()));
        }

        public async Task<StatusCode> AddReservation(int idRoom)
        {
            return StatusCode.Error;
            //return await this.reservationRepository.AddReservation(idRoom, this.currentReservation);
        }

        public async Task<StatusCode> DeleteReservation(int idReservation)
        {
            return await this.reservationRepository.RemoveReservation(idReservation);
        }

        public List<ReservationModel> Sort(List<ReservationModel> list)
        {
            User currentUser = new User();
            currentUser.MyRoom = "303";
            currentUser.FavoriteRoom = "505";
            int userFloor = this.GetFloorInNumber(currentUser.MyRoom);
            list.Sort((view1, view2) =>
            {
                if (Math.Abs(GetFloorInNumber(view1.Room.ToString()) - userFloor) >
                    Math.Abs(GetFloorInNumber(view2.Room.ToString()) - userFloor))
                {
                    return 1;
                }
                else
                {
                    if (Math.Abs(GetFloorInNumber(view1.Room.ToString()) - userFloor) <
                        Math.Abs(GetFloorInNumber(view2.Room.ToString()) - userFloor))
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            });
            if (list.FindIndex(x => x.Room.Number == currentUser.FavoriteRoom) > 0)
            {
                ReservationModel first = list[list.FindIndex(x => x.Room.Number == currentUser.FavoriteRoom)];
                list.Remove(first);
                list.Insert(0, first);
            }

            return list;
        }

        public int GetFloorInNumber(string s)
        {
            string stringFloor = new string(s.ToCharArray().Where(ch => !char.IsLetter(ch)).ToArray());
            if (stringFloor.Length < 4)
            {
                return int.Parse(stringFloor[0].ToString());
            }
            else
            {
                return int.Parse(stringFloor[0].ToString() + stringFloor[1]);
            }
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
            this.currentReservation = new ReservationModel()
            {
                Author = SelectPage.CurrentUser.Login,
                From = DateTime.Now,
                To = DateTime.Now.AddHours(1),
            };
            return await this.reservationRepository.AddReservation(int.Parse(text), this.currentReservation);
        }
    }
}