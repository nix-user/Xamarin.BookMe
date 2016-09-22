using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
using BookMeMobile.Pages.MyBookPages;
using BookMeMobile.Resources;
using Java.Sql;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class SelectViewModel : BaseViewModel
    {
        private SelectModel model;
        private ListRoomManager service;

        public SelectViewModel()
        {
            this.model = new SelectModel();
            this.service = new ListRoomManager();
            this.GoToMyReservation = new Command(this.GetMyReservation);
            this.GoToSearch = new Command(this.Search);
        }

        public ICommand GoToMyReservation { get; protected set; }

        public ICommand GoToSearch { get; protected set; }

        public async void Search(object element)
        {
            if (this.model.From < this.model.To)
            {
                var operationResult =
                   (await this.ExecuteOperation(async () => await this.service.GetEmptyRoom(this.model)));

                if (operationResult.Status == StatusCode.Ok)
                {
                    await this.Navigation.PushAsync(new MainPage(new ListRoomPage(operationResult.Result, this.model)));
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

        public async void GetMyReservation(object element)
        {
            var operationResult = (await this.ExecuteOperation(async () => await this.service.GetAllUserReservation()));
            if (operationResult.Status == StatusCode.Ok)
            {
                await this.Navigation.PushAsync(new TabPanelPage(operationResult.Result));
            }
            else
            {
                this.ShowErrorMessage(operationResult.Status);
            }
        }

        public DateTime Date
        {
            get { return this.model.Date; }
            set { this.model.Date = value; }
        }

        public TimeSpan From
        {
            get { return this.model.From.TimeOfDay; }
            set { this.model.From = Convert.ToDateTime(value); }
        }

        public DateTime To
        {
            get { return this.model.To; }
            set { this.model.To = Convert.ToDateTime(value); }
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
    }
}