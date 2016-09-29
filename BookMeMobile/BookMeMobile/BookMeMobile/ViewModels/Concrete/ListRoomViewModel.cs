﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Entity;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Model
{
    public class ListRoomViewModel : BaseViewModel
    {
        private SelectModel selectModel;

        public List<RoomViewModel> ListRoom { get; }

        public ICommand ReserveCommand { get; set; }

        public string IsNotRooms
        {
            get { return this.ListRoom.Any() ? null : "Комнат нет"; }
        }

        public ListRoomViewModel(IEnumerable<Room> rooms, INavigationService navigationService, SelectModel selectModel) : base(navigationService)
        {
            this.ListRoom = rooms.Select(x => new RoomViewModel(x)).ToList();
            this.ReserveCommand = new Command(this.Reserve);
            this.selectModel = selectModel;
        }

        public void Reserve(object selectElement)
        {
            RoomViewModel selectedRoom = selectElement as RoomViewModel;
            this.NavigationService.ShowViewModel(new AddReservationViewModel(this.selectModel, this.NavigationService, selectedRoom));
        }
    }
}