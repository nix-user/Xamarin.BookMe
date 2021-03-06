﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Data.FakeRepository;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

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
            this.reservationRepository = new FakeReservationRepository(new HttpService(new CustomDependencyService(), new HttpClientHandler()));
            this.roomRepository = new FakeRoomRepository(new HttpService(new CustomDependencyService(), new HttpClientHandler()));
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