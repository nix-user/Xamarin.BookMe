using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ListRoomPage : ContentPage
    {
        private static readonly IEnumerable<string> UnallowedResources = new List<string>()
        {
            "709",
            "710",
            "712a",
            "713",
            "Netatmo",
            "NetatmoCityHall",
            "Projector 1",
            "Projector 2"
        };

        public List<Room> ResultRoom { get; set; }

        private Reservation currentBooking;
        private ListRoomManager manager;
        private SelectModel currentReservation;

        public ListRoomPage(IEnumerable<Room> search, SelectModel reservation)
        {
            this.InitializeComponent();
            this.manager = new ListRoomManager();
            this.ResultRoom = search.Where(x => !UnallowedResources.Contains(x.Number)).ToList();
            this.manager.Sort(this.ResultRoom);
            if (!this.ResultRoom.Any())
            {
                isRoom.IsVisible = true;
            }

            this.currentReservation = reservation;

            listUserRoomInRange.BindingContext = this.ResultRoom;
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idRoom = int.Parse(((Button)sender).ClassId);
            await this.Navigation.PushModalAsync(new AddReservationPage(this.currentReservation, idRoom));
        }
    }
}