﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
using BookMeMobile.Pages.MyReservations;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Abstract;
using Java.Sql;
using Javax.Security.Auth;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class SelectViewModel : BaseViewModel
    {
        private SelectModel model;
        private ListRoomManager service;

        public SelectViewModel(ListRoomManager listRoomManager, INavigationService navigationService) : base(navigationService)
        {
            this.model = new SelectModel();
            this.service = listRoomManager;

            this.GoToMyReservation = new Command(this.GetMyReservation);
            this.GoToSearch = new Command(this.Search);
        }

        public ICommand GoToMyReservation { get; protected set; }

        public ICommand GoToSearch { get; protected set; }

        private async void Search()
        {
            if (this.model.From < this.model.To)
            {
                var operationResult =
                   (await this.ExecuteOperation(async () => await this.service.GetEmptyRoom(this.model)));

                if (operationResult.Status == StatusCode.Ok)
                {
                    await this.NavigationService.XamarinNavigation.PushAsync(new MainPage(new ListRoomPage(operationResult.Result, this.model)));
                }
                else
                {
                    this.ShowErrorMessage(operationResult.Status);
                }
            }
            else
            {
                this.ShowInformationDialog(AlertMessages.ErrorHeader, AlertMessages.WrongIntervalTime);
            }
        }

        private async void GetMyReservation()
        {
            await this.NavigationService.XamarinNavigation.PushAsync(new MyReservationsPage());
        }

        public DateTime Date
        {
            get { return this.model.Date; }
            set { this.model.Date = value; }
        }

        public DateTime From
        {
            get { return this.model.From; }
            set { this.model.From = this.SetValidDate(value); }
        }

        public DateTime To
        {
            get { return this.model.To; }
            set { this.model.To = this.SetValidDate(value); }
        }

        public bool IsLarge
        {
            get { return this.model.IsLarge; }
            set { this.model.IsLarge = value; }
        }

        public bool HasPolycom
        {
            get { return this.model.HasPolycom; }
            set { this.model.HasPolycom = value; }
        }

        private DateTime SetValidDate(DateTime invalidDate)
        {
            return new DateTime(this.Date.Year, this.Date.Month, this.Date.Day, invalidDate.Hour, invalidDate.Minute, invalidDate.Second);
        }
    }
}