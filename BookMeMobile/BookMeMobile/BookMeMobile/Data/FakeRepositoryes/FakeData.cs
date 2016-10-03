using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.FakeRepository
{
    public class FakeData
    {
        private static List<Room> allRoom = new List<Room>()
        {
            new Room() { Id = 1, IsBig = false, IsHasPolykom = true, Number = "404e" },
            new Room() { Id = 2, IsBig = false, IsHasPolykom = true, Number = "505d" },
            new Room() { Id = 3, IsBig = true, IsHasPolykom = false, Number = "345a" },
            new Room() { Id = 4, IsBig = false, IsHasPolykom = true, Number = "601d" },
            new Room() { Id = 5, IsBig = true, IsHasPolykom = true, Number = "422b" },
        };

        private static List<Reservation> allReservation = new List<Reservation>()
        {
            new Reservation()
            {
                From = new DateTime(2016, 02, 04),
                To = new DateTime(2016, 02, 04),
                Duration = new TimeSpan(0, 30, 0),
                Title = "Нужна комната 1",
                Room = new Room() { Id = 1, IsHasPolykom = true, IsBig = true, Number = "420e" },
                IsRecursive = false
            },
             new Reservation()
            {
                From = new DateTime(2016, 03, 04),
                To = new DateTime(2016, 03, 04),
                Duration = new TimeSpan(0, 30, 0),
                Title = "Нужна комната 2 ",
                Room = new Room() { Id = 1, IsHasPolykom = true, IsBig = true, Number = "440e" },
                IsRecursive = false
            },
              new Reservation()
            {
                From = new DateTime(2016, 03, 04),
                To = new DateTime(2016, 03, 04),
                Duration = new TimeSpan(0, 30, 0),
                Title = "Нужна комната 3 ",
                Room = new Room() { Id = 1, IsHasPolykom = true, IsBig = true, Number = "460e" },
                IsRecursive = false
            },
               new Reservation()
            {
                From = new DateTime(2016, 04, 04),
                To = new DateTime(2016, 04, 04),
                Duration = new TimeSpan(0, 30, 0),
                Title = "Нужна комната 4 ",
                Room = new Room() { Id = 1, IsHasPolykom = true, IsBig = true, Number = "470e" },
                IsRecursive = false
            }
        };

        public static BaseOperationResult<IEnumerable<Reservation>> ReservationList => new BaseOperationResult<IEnumerable<Reservation>>()
        {
            Status = StatusCode.Ok,
            Result = allReservation
        };

        public static BaseOperationResult<Reservation> Reservation => new BaseOperationResult<Reservation>()
        {
            Status = StatusCode.Ok,
            Result = allReservation.FirstOrDefault()
        };

        public static BaseOperationResult<IEnumerable<Room>> ListRoomResult => new BaseOperationResult<IEnumerable<Room>>()
        {
            Status = StatusCode.Ok,
            Result = allRoom
        };

        public static BaseOperationResult<Room> Room => new BaseOperationResult<Room>()
        {
            Status = StatusCode.Ok,
            Result = allRoom.FirstOrDefault()
        };

        public static BaseOperationResult<UserReservationsModel> UserReservation => new BaseOperationResult<UserReservationsModel>
        {
            Status = StatusCode.Ok,
            Result = userreservationModel
        };

        private static UserReservationsModel userreservationModel = new UserReservationsModel()
        {
            AllReservations = allReservation,
            TodayReservations = allReservation.Where(x => x.To == new DateTime(2016, 03, 04))
        };

        public static BaseOperationResult SuccessResult()
        {
            return new BaseOperationResult() { Status = StatusCode.Ok };
        }

        public static BaseOperationResult<ProfileModel> ProfileModel()
        {
            return new BaseOperationResult<ProfileModel>()
            {
                Status = StatusCode.Ok,
                Result = new ProfileModel() { FavouriteRoom = "1421e", Floor = 4 }
            };
        }
    }
}