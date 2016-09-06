using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using Org.Apache.Http.Impl.Cookie;

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

        public ReservationsStatusModel AddUserReservationInRange(ReservationModel reservation)
        {
            try
            {
                List<MyReservationViewResult> result = new List<MyReservationViewResult>();
                foreach (
                    Room room in
                        this.roomRepository.GetAllRoom().Result
                            .Where(x => x.IsBig == reservation.Room.IsBig && x.IsHasPolykom == reservation.Room.IsHasPolykom))
                {
                    foreach (
                        var currentReservation in
                            room.Reservations.Where(
                                x => x.Author.Id == reservation.Author.Id && x.Date == reservation.Date))
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

                return new ReservationsStatusModel() { ReservationModels = result, StatusCode = StatusCode.Ok };
            }
            catch (AggregateException)
            {
                return new ReservationsStatusModel() { ReservationModels = null, StatusCode = StatusCode.NoInternet };
            }
        }

        public ReservationsStatusModel AddUserReservationPartRange(ReservationModel reservation)
        {
            try
            {
                List<MyReservationViewResult> result = new List<MyReservationViewResult>();
                foreach (
                    Room room in
                        this.roomRepository.GetAllRoom().Result
                            .Where(
                                x =>
                                    x.IsBig == reservation.Room.IsBig && x.IsHasPolykom == reservation.Room.IsHasPolykom))
                {
                    foreach (var currentReserve in room.Reservations)
                    {
                        bool endInRange = currentReserve.From <= reservation.From &&
                                          currentReserve.To >= reservation.From && reservation.To >= currentReserve.To;
                        bool startInRange = currentReserve.From <= reservation.To && currentReserve.To >= reservation.To &&
                                            reservation.From <= currentReserve.From;
                        if ((endInRange | startInRange) & currentReserve.Author.Id == reservation.Author.Id &&
                            currentReserve.Date == reservation.Date)
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

                return new ReservationsStatusModel() { ReservationModels = this.Sort(result), StatusCode = StatusCode.Ok };
            }
            catch (AggregateException)
            {
                return new ReservationsStatusModel() { ReservationModels = null, StatusCode = StatusCode.NoInternet };
            }
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

        public ReservationsStatusModel Search(ReservationModel reservation)
        {
            try
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
                        if (!(currenrReservation.From >= reservation.To || currenrReservation.To <= reservation.From))
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

                return new ReservationsStatusModel() { ReservationModels = this.Sort(result), StatusCode = StatusCode.Ok };
            }
            catch (AggregateException)
            {
                return new ReservationsStatusModel() { ReservationModels = null, StatusCode = StatusCode.NoInternet };
            }
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

        public async Task<StatusCode> AddReservation(int idRoom)
        {
            this.currentReservation.Id = counter++;
            return await this.reservationRepository.AddReservation(this.currentReservation);
        }

        public async Task<StatusCode> AddReservation(ReservationModel reservation)
        {
            reservation.Id = counter++;
            return await this.reservationRepository.AddReservation(reservation);
        }

        public async Task<StatusCode> DeleteReservation(int idReservation)
        {
            return await this.reservationRepository.RemoveReservation(idReservation);
        }

        public async Task<ReservationsStatusModel> GetUserReservation()
        {
            try
            {
                List<MyReservationViewResult> result = new List<MyReservationViewResult>();
                IEnumerable<ReservationModel> allReservation = await this.reservationRepository.GetAll();

                if (allReservation == null)
                {
                    return new ReservationsStatusModel() { ReservationModels = null, StatusCode = StatusCode.Error };
                }

                foreach (ReservationModel reservation in allReservation.Where(x => x.IsRecursive == false))
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

                return new ReservationsStatusModel() { ReservationModels = result, StatusCode = StatusCode.Ok };
            }
            catch (AggregateException)
            {
                return new ReservationsStatusModel() { ReservationModels = null, StatusCode = StatusCode.NoInternet };
            }
        }

        public async Task<ReservationsStatusModel> GetUserReservationingsRecursive()
        {
            try
            {
                List<MyReservationViewResult> result = new List<MyReservationViewResult>();

                IEnumerable<ReservationModel> allReservation = await this.reservationRepository.GetAll();

                if (allReservation == null)
                {
                    return new ReservationsStatusModel() { ReservationModels = null, StatusCode = StatusCode.Error };
                }

                allReservation = allReservation.Where(x => x.Author.Id == this.currentUser.Id && x.IsRecursive);
                result =
                    allReservation.GroupBy(x => new { x.From, x.To, x.Room }).Select(x => new MyReservationViewResult()
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

                return new ReservationsStatusModel() { ReservationModels = result, StatusCode = StatusCode.Ok };
            }
            catch (AggregateException)
            {
                return new ReservationsStatusModel() { ReservationModels = null, StatusCode = StatusCode.NoInternet };
            }
        }

        public async Task<StatusCode> DeleteReservationRecursive(int idReservation)
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

        public ReservationStatusModel AttemptReservation(string result, User user)
        {
            try
            {
                bool roomReserve = false;
                Room currentRoom = this.roomRepository.GetAllRoom().Result.FirstOrDefault(x => x.Number == result);
                foreach (
                    ReservationModel currentReservation in
                        currentRoom.Reservations.Where(x => x.Date.Date == DateTime.Now.Date))
                {
                    if (currentReservation.From >= DateTime.Now.TimeOfDay ||
                        currentReservation.To <= DateTime.Now.TimeOfDay)
                    {
                        roomReserve = false;
                    }
                    else
                    {
                        if (currentReservation.Author.Id == this.currentUser.Id)
                        {
                            return new ReservationStatusModel()
                            {
                                Reservation = new ReservationModel()
                                {
                                    Date = DateTime.Now,
                                    Room = currentRoom,
                                    From = currentReservation.To,
                                    Id = DateTime.Now.Millisecond + new Random().Next(1000),
                                    To = currentReservation.To.Add(new TimeSpan(1, 0, 0)),
                                    Author = this.currentUser
                                },
                                StatusCode = StatusCode.Ok
                            };
                        }
                        else
                        {
                            roomReserve = true;
                            return null;
                        }
                    }
                }

                return this.NewReservationModel(currentRoom);
            }
            catch (AggregateException)
            {
                return new ReservationStatusModel() { Reservation = null, StatusCode = StatusCode.NoInternet };
            }
        }

        private ReservationStatusModel NewReservationModel(Room currentRoom)
        {
            return new ReservationStatusModel()
            {
                Reservation = new ReservationModel()
                {
                    Date = DateTime.Now,
                    Room = currentRoom,
                    From = DateTime.Now.Subtract(DateTime.Now.Date),
                    Id = DateTime.Now.Millisecond + new Random().Next(1000),
                    To = DateTime.Now.AddHours(1).Subtract(DateTime.Now.Date),
                    Author = this.currentUser
                },
                StatusCode = StatusCode.Ok
            };
        }
    }
}