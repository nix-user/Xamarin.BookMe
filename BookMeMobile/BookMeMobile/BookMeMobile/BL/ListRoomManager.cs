using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Entity;

namespace BookMeMobile.BL
{
    public class ListRoomManager
    {
        private static int counter = 0;

        private RoomRepository roomRepository;
        private ReservationRepository reservationRepository;

        private ReservationModel currentReservation;
        private User currentUser;

        public ListRoomManager(ReservationModel reservation, User currentUser)
        {
            this.reservationRepository = new ReservationRepository();
            this.roomRepository = new RoomRepository();
            this.currentReservation = reservation;
            this.currentUser = currentUser;
        }

        public ListRoomManager(User user)
        {
            this.reservationRepository = new ReservationRepository();
            this.roomRepository = new RoomRepository();
            this.currentUser = user;
        }

        public List<MyReservationViewResult> AddUserReservationInRange(ReservationModel reservation)
        {
            List<MyReservationViewResult> result = new List<MyReservationViewResult>();
            foreach (
                Room room in
                    this.roomRepository.GetAllRoom().Result
                        .Where(x => x.IsBig == reservation.Room.IsBig && x.IsHasPolykom == reservation.Room.IsHasPolykom))
            {
                foreach (var currentReservation in room.Reservations.Where(x => x.Author.Id == reservation.Author.Id && x.Date == reservation.Date))
                {
                    if (currentReservation.From < reservation.From && currentReservation.To > reservation.To)
                    {
                        result.Add(new MyReservationViewResult()
                        {
                            From = currentReservation.From,
                            To = currentReservation.To,
                            Room = currentReservation.Room.Number,
                            InRange = true,
                            IsReservation = true
                        });
                    }
                }
            }

            return result;
        }

        public List<MyReservationViewResult> AddUserReservationPartRange(ReservationModel reservation)
        {
            List<MyReservationViewResult> result = new List<MyReservationViewResult>();
            foreach (
                Room room in
                    this.roomRepository.GetAllRoom().Result
                        .Where(x => x.IsBig == reservation.Room.IsBig && x.IsHasPolykom == reservation.Room.IsHasPolykom))
            {
                foreach (var currentReserve in room.Reservations)
                {
                    bool endInRange = currentReserve.From <= reservation.From && currentReserve.To >= reservation.From && reservation.To >= currentReserve.To;
                    bool startInRange = currentReserve.From <= reservation.To && currentReserve.To >= reservation.To && reservation.From <= currentReserve.From;
                    if ((endInRange | startInRange) & currentReserve.Author.Id == reservation.Author.Id && currentReserve.Date == reservation.Date)
                    {
                        result.Add(new MyReservationViewResult()
                        {
                            Date = currentReserve.Date,
                            From = currentReserve.From,
                            To = currentReserve.To,
                            Room = currentReserve.Room.Number,
                            IsHasPolykom = room.IsHasPolykom,
                            IsBig = room.IsBig,
                            InRange = false,
                            IsReservation = true
                        });
                    }
                }
            }

            return this.Sort(result);
        }

        private List<Room> ConditionTrue(ReservationModel reservation)
        {
            if (reservation.Room.IsBig && reservation.Room.IsHasPolykom)
            {
                return this.roomRepository.GetAllRoom().Result.Where(x => x.IsBig && x.IsHasPolykom).ToList();
            }

            if (!reservation.Room.IsBig && reservation.Room.IsHasPolykom)
            {
                return this.roomRepository.GetAllRoom().Result.Where(x => (x.IsBig || !x.IsBig) && x.IsHasPolykom).ToList();
            }

            if (reservation.Room.IsBig && !reservation.Room.IsHasPolykom)
            {
                return this.roomRepository.GetAllRoom().Result.Where(x => (x.IsHasPolykom || !x.IsHasPolykom) && x.IsBig).ToList();
            }

            if (!reservation.Room.IsBig && !reservation.Room.IsHasPolykom)
            {
                return this.roomRepository.GetAllRoom().Result.Where(x => true).ToList();
            }

            return null;
        }

        public List<MyReservationViewResult> Search(ReservationModel reservation)
        {
            List<MyReservationViewResult> result = new List<MyReservationViewResult>();
            List<Room> srchList = this.ConditionTrue(reservation);

            foreach (var item in srchList)
            {
                List<ReservationModel> searchReservations = new List<ReservationModel>();
                bool hasRange = true;
                if (reservation.IsRecursive)
                {
                    searchReservations.AddRange(item.Reservations.Where(x => x.Date >= reservation.Date));
                }
                else
                {
                    searchReservations.AddRange(item.Reservations.Where(x => x.Date.Date == reservation.Date.Date));
                }

                foreach (ReservationModel currenrReservation in searchReservations)
                {
                    if (currenrReservation.From >= reservation.To || currenrReservation.To <= reservation.From)
                    {
                        hasRange = true;
                    }
                    else
                    {
                        hasRange = false;
                        break;
                    }
                }

                if (hasRange)
                {
                    result.Add(new MyReservationViewResult()
                    {
                        IsHasPolykom = item.IsHasPolykom,
                        IsBig = item.IsBig,
                        Room = item.Number,
                        Id = item.Id,
                        IsReservation = false,
                        InRange = null
                    });
                }
            }

            return this.Sort(result);
        }

        public async Task<string> ReservationMessag(int idRoom)
        {
            this.currentReservation.Room = await this.roomRepository.GetRoom(idRoom);
            Room currentRoom = this.roomRepository.GetRoom(idRoom).Result;
            return string.Format(
                " Комната: {3}\n Дата: {0}\n Время: {1} - {2}\n Большая:{4} Поликом:{5}",
                this.currentReservation.Date.ToString("d"),
                this.currentReservation.From.ToString(@"hh\:mm"),
                this.currentReservation.To.ToString(@"hh\:mm"),
                currentRoom.Number,
                string.Format("{0:Да;0;Нет}", currentRoom.IsBig.GetHashCode()),
                string.Format("{0:Да;0;Нет}", currentRoom.IsHasPolykom.GetHashCode()));
        }

        public async void AddRecursiveReservation(int idRoom)
        {
            for (int i = 1; i <= 7; i++)
            {
                ReservationModel currentReservation = new ReservationModel()
                {
                    IsRecursive = true,
                    Room = this.currentReservation.Room,
                    Id = this.currentReservation.Id,
                    Date = this.currentReservation.Date,
                    From = this.currentReservation.From,
                    To = this.currentReservation.To,
                    Author = this.currentReservation.Author
                };
                this.currentReservation.Id = counter++;
                this.roomRepository.GetAllRoom().Result.FirstOrDefault(x => x.Id == idRoom).Reservations.Add(currentReservation);
                await this.reservationRepository.AddReservation(currentReservation);
                this.currentReservation.Date = DateTime.Now.AddDays(i);
            }
        }

        public async void AddReservation(int idRoom)
        {
            this.currentReservation.Id = counter++;
            await this.reservationRepository.AddReservation(this.currentReservation);
        }

        public async void AddReservation(ReservationModel reservation)
        {
            reservation.Id = counter++;
            await this.reservationRepository.AddReservation(reservation);
        }

        public async Task<bool> DeleteReservation(int idReservation)
        {
            ReservationModel deleteReservation = this.reservationRepository.GetReservation(idReservation).Result;
            return await this.reservationRepository.RemoveReservation(deleteReservation.Id);
        }

        public List<MyReservationViewResult> GetUserReservation()
        {
            List<MyReservationViewResult> result = new List<MyReservationViewResult>();

            foreach (ReservationModel reservation in this.reservationRepository.GetAll().Result.Where(x => x.IsRecursive == false))
            {
                if (reservation.Author.Id == this.currentUser.Id)
                {
                    result.Add(new MyReservationViewResult()
                    {
                        Room = reservation.Room.Number,
                        Date = reservation.Date,
                        From = reservation.From,
                        To = reservation.To,
                        IsHasPolykom = reservation.Room.IsHasPolykom,
                        IsBig = reservation.Room.IsBig,
                        IsReservation = false,
                        Id = reservation.Id
                    });
                }
            }

            return result;
        }

        public List<MyReservationViewResult> GetUserReservationingsRecursive()
        {
            List<MyReservationViewResult> result = new List<MyReservationViewResult>();
            List<ReservationModel> allReservationOne = this.reservationRepository.GetAll().Result.Where(x => x.Author.Id == this.currentUser.Id && x.IsRecursive).ToList();

            result = allReservationOne.GroupBy(x => new { x.From, x.To, x.Room }).Select(x => new MyReservationViewResult()
            {
                Id = x.First().Id,
                From = x.Key.From,
                To = x.Key.To,
                Room = x.Key.Room.Number,
                Date = x.First().Date,
                IsRecursive = true,
                IsBig = x.First().Room.IsBig,
                IsHasPolykom = x.FirstOrDefault().Room.IsHasPolykom
            }).ToList();

            return result;
        }

        public async Task<bool> DeleteReservationRecursive(int idReservation)
        {
            return await this.reservationRepository.RemoveReservation(idReservation);
        }

        public List<MyReservationViewResult> Sort(List<MyReservationViewResult> list)
        {
            int userFloor = this.GetFloorInNumber(this.currentUser.MyRoom);
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
            if (list.FindIndex(x => x.Room == this.currentUser.FavoriteRoom) > 0)
            {
                MyReservationViewResult first = list[list.FindIndex(x => x.Room == this.currentUser.FavoriteRoom)];
                list.Remove(first);
                list.Insert(0, first);
            }

            return list;
        }

        public int GetFloorInNumber(string s)
        {
            string stringFloor = new string(s.ToCharArray().Where(ch => !char.IsLetter(ch)).ToArray());
            int floor;
            if (stringFloor.Length < 4)
            {
                return floor = int.Parse(stringFloor[0].ToString());
            }
            else
            {
                return floor = int.Parse(stringFloor[0].ToString() + stringFloor[1]);
            }
        }

        public ReservationModel AttemptReservation(string result, User user)
        {
            bool roomReserve = false;
            Room currentRoom = this.roomRepository.GetAllRoom().Result.FirstOrDefault(x => x.Number == result);
            foreach (ReservationModel currentReservation in currentRoom.Reservations.Where(x => x.Date.Date == DateTime.Now.Date))
            {
                if (currentReservation.From >= DateTime.Now.TimeOfDay || currentReservation.To <= DateTime.Now.TimeOfDay)
                {
                    roomReserve = false;
                }
                else
                {
                    if (currentReservation.Author.Id == this.currentUser.Id)
                    {
                        return new ReservationModel()
                        {
                            Date = DateTime.Now,
                            Room = currentRoom,
                            From = currentReservation.To,
                            Id = DateTime.Now.Millisecond + new Random().Next(1000),
                            To = currentReservation.To.Add(new TimeSpan(1, 0, 0)),
                            Author = this.currentUser
                        };
                    }
                    else
                    {
                        roomReserve = true;
                        return null;
                    }
                }
            }

            return new ReservationModel()
            {
                Date = DateTime.Now,
                Room = currentRoom,
                From = DateTime.Now.Subtract(DateTime.Now.Date),
                Id = DateTime.Now.Millisecond + new Random().Next(1000),
                To = DateTime.Now.AddHours(1).Subtract(DateTime.Now.Date),
                Author = this.currentUser
            };
        }
    }
}